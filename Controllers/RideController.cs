using Microsoft.AspNetCore.Mvc;
using RentalRides.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalRides.Controllers
{
    public class RideController : Controller
    {
        public IActionResult Index()
        {
            var segments = new List<Segment>();
            for (var i = 1; i <=10; i++)
            {
                segments.Add(new Segment { SegmentId = i, Name = "Segment " + i.ToString() });
            }

            return View(segments);
        }

        public IActionResult Browse(String segment)
        {
            ViewBag.segment = segment;
            return View();
        }

        // /Store/AddCategory
        public IActionResult AddSegment()
        {
            //load a form to capture a object from the user
            return View();
        }
    }
}
