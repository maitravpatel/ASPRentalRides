using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalRides.Models
{
    public class Car
    {
        public int CarId { get; set; } //PK
        public string Name { get; set; }
        public string Description { get; set; }
        public int Photo { get; set; }
        public decimal Price { get; set; }
        public Segment SegmentId { get; set; }

        public List<BookingDetail> BookingDetails { get; set; }

        public List<Cart> Carts { get; set; }
    }
}
