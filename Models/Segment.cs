using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentalRides.Models
{
    public class Segment
    {
        public int SegmentId { get; set; } //Pk
        [Required]
        public string Name { get; set; } 
        public List<Car> Cars { get; set; }
    }
}
