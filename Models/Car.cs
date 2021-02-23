using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentalRides.Models
{
    public class Car
    {
        public int CarId { get; set; } //PK
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Photo { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [Range(0.01,9999999)]
        public double Price { get; set; }
        public Segment Segment { get; set; }
        [Display(Name="Category")]
        public int SegmentId { get; set; }

        public List<BookingDetail> BookingDetails { get; set; }

        public List<Cart> Carts { get; set; }
    }
}
