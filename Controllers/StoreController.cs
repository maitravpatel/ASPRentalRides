using Microsoft.AspNetCore.Mvc;
using RentalRides.Data;
using System;
using System.Collections.Generic;
using RentalRides.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

//Stripe Payment
using Stripe;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Stripe.Checkout;

namespace RentalRides.Controllers
{
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext _context;

        IConfiguration _iconfiguration;

        public StoreController(ApplicationDbContext context, IConfiguration iconfiguration)
        {
            _context = context;
            _iconfiguration = iconfiguration;
        }

        public IActionResult Index()
        {
            //get a list of segments to display to customers
            var segmemts = _context.Segments.OrderBy(s => s.Name).ToList();
            return View(segmemts);
        }

        //Store/Browse
        public IActionResult Browse(int id)
        {
            //Query products for the selected store
            var cars = _context.Cars.Where(c => c.SegmentId == id).OrderBy(c => c.Name).ToList();

            //Get name of the selected category.
            ViewBag.segment = _context.Segments.Find(id).Name.ToString();
            return View(cars);
        }

        public IActionResult AddToCart(int CarId, int Quantity)
        {
            //query the db for car price
            var price = _context.Cars.Find(CarId).Price;

            //get current date and time
            var currentDateTime = DateTime.Now;

            //customerId Variable
            var customerId = GetCustomerId();

            //create and save a new Cart Object
            var cart = new Cart
            {
                CarId = CarId,
                Quantity = Quantity,
                Price = price,
                BookedDate = currentDateTime,
                CustomerId = customerId
            };

            _context.Carts.Add(cart);
            _context.SaveChanges();

            //redirect to the Cart View
            return RedirectToAction("Cart");

        }

        private string GetCustomerId()
        {
            //check the session for existing CustomerId
            if (HttpContext.Session.GetString("CustomerId") == null)
            {
                //if we don't already have an existing CustomerId in the session, check if customer is logged in
                var CustomerId = "";

                //if customer is logged in, use their email as CustomerId
                if (User.Identity.IsAuthenticated)
                {
                    CustomerId = User.Identity.Name;
                }

                //if the customer is anonymou, use GUID to create a new identifier
                else
                {
                    CustomerId = Guid.NewGuid().ToString();
                }

                //now store the customerId in a session variable
                HttpContext.Session.SetString("CustomerId", CustomerId);
            }
            //return the session variable
            return HttpContext.Session.GetString("CustomerId");
        }

        //Store/Cart
        public IActionResult Cart()
        {
            //fetch the current cart for display
            var CustomerId = "";
            //in case user comes to cart before adding anything, identify them first
            if (HttpContext.Session.GetString("CustomerId") == null)
            {
                CustomerId = GetCustomerId();
            }
            else
            {
                CustomerId = HttpContext.Session.GetString("CustomerId");
            }

            //query the db for this customer
            var cartItems = _context.Carts.Include(c => c.Car).Where(c => c.CustomerId == CustomerId).ToList();

            //pass the data to view for display


            return View(cartItems);
        }

        //GET /Store/RemoveFromCart
        public IActionResult RemoveFromCart(int id)
        {
            var cartItem = _context.Carts.Find(id);

            if(cartItem != null)
            {
                _context.Carts.Remove(cartItem);
                _context.SaveChanges();
            }

            return RedirectToAction("Cart");
        }

        //Store/checkout
        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }

        //POST: //Store/Checkout
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Checkout([Bind("FirstName,LastName,Address, City, Province, PostalCode")] Models.BookingList bookingList)
        {
            //Populate the 3 automatic properties
            bookingList.BookingTime = DateTime.Now;
            bookingList.CustomerId = User.Identity.Name;
            bookingList.Total = (from c in _context.Carts
                                 where c.CustomerId == HttpContext.Session.GetString("CustomerId")
                                 select c.Quantity * c.Price).Sum();


            //use SessionExtension object to store the obj in a session variable
            HttpContext.Session.SetObject("BookingList", bookingList);

            //redirect to payment page
            return RedirectToAction("Payment");

        }
        //GET
        public IActionResult Payment()
        {
            var bookingList = HttpContext.Session.GetObject<Models.BookingList>("BookingList");

            ViewBag.Total = bookingList.Total;

            ViewBag.PublishableKey = _iconfiguration.GetSection("Stripe")["PublishableKey"];

            return View();
        }

        //POST /Store/ProcessPayment
        [Authorize]
        [HttpPost]
        public IActionResult ProcessPayment()
        {
            var bookingList = HttpContext.Session.GetObject<Models.BookingList>("BookingList");

            StripeConfiguration.ApiKey = _iconfiguration.GetSection("Stripe")["SecretKey"];

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long?)(bookingList.Total * 100),
                            Currency = "cad",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name="Rental Rides Booking",
                            },
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = "https://" + Request.Host + "/Store/SaveOrder",
                CancelUrl = "https://" + Request.Host + "/Store/Cart",
            };
            var service = new SessionService();
            Session session = service.Create(options);
            return Json(new { id = session.Id });

        }
        
        [Authorize]
        public IActionResult SaveOrder()
        {
            var bookingList = HttpContext.Session.GetObject<Models.BookingList>("Booking List");

            _context.BookingLists.Add(bookingList);
            _context.SaveChanges();

            var cartItems = _context.Carts.Where(c => c.CustomerId == HttpContext.Session.GetString("CustomerId"));
            foreach(var item in cartItems)
            {
                var bookingDetail = new BookingDetail
                {
                    CarId = item.CarId,
                    TotalCost = (decimal)(item.Price * item.Quantity),
                    BookingListId = bookingList.BookingListId
                };

                _context.BookingDetails.Add(bookingDetail);
            }
            _context.SaveChanges();

            //delete the items from user's cart
            foreach(var item in cartItems)
            {
                _context.Carts.Remove(item);
            }

            _context.SaveChanges();

            //HttpContext.Session.SetInt32("ItemCount", 0);

            return RedirectToAction("Details", "Orders", new { @id = bookingList.BookingListId });
        }
    }
}
