using Microsoft.AspNetCore.Mvc;
using RentalRides.Data;
using System;
using System.Collections.Generic;
using RentalRides.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace RentalRides.Controllers
{
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoreController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //get a list of segments to display to customers
            var segmemts = _context.Segments.OrderBy(s => s.Name).ToList();
            return View(segmemts);
        }

        public IActionResult Browse(int id)
        {
            //Query products or the selected store
            var rides = _context.Cars.Where(c => c.SegmentId == id).OrderBy(c => c.Name).ToList();

            //Get name of the selected category.


            ViewBag.Segment = _context.Segments.Find(id).Name.ToString();
            return View();
        }
    }
}
