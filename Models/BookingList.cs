using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentalRides.Models
{
    public class BookingList
    {
        public int BookingListId { get; set; }  //Pk
        public DateTime BookingTime { get; set; }
        public string CustomerId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string email { get; set; }
        public double Total { get; set; }

        //Child Reference
        public List<BookingDetail> BookingDetails { get; set; }


    }
}
