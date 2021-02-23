using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalRides.Models
{
    public class BookingList
    {
        public int BookingListId { get; set; }  //Pk
        public DateTime BookingTime { get; set; }
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string email { get; set; }

        //Child Reference
        public List<BookingDetail> BookingDetails { get; set; }


    }
}
