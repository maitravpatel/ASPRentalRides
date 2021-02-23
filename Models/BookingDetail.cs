using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalRides.Models
{
    public class BookingDetail
    {
        public int BookingDetailId { get; set; } //pk
        public int CarId { get; set; } //pk
        public int BookingListId { get; set; } //pk
        public DateTime PickupTime { get; set; }
        public String PickupLocation { get; set; }
        public DateTime DropOffTime { get; set; }
        public String DropOffLocation { get; set; }
        public Decimal TotalCost { get; set; }

        public BookingList BookingList { get; set; }

    }
}
