using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalRides.Models
{
    public class Cart
    {
        public int CartId { get; set; } //pk
        public int CarId { get; set; }
        public DateTime BookedDate { get; set; }
        public string CustomerId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public Car Car { get; set; }
    }
}
