using Microsoft.AspNetCore.Mvc;
using RentalRides.Data;
using System;
using System.Collections.Generic;
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
    }
}
