using BookCheckInOut.Core.Checkins;
using BookCheckInOut.Core.Checkouts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookCheckInOut.Web.Dtos.Persons
{
    public class PersonDetailDto
    {
        public long Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public string MobileNo { get; set; }

        [Range(0, 11, ErrorMessage = "Must be a positive number and 11 digits")]
        public int NationalID { get; set; }
        public decimal PenaltyFee { get; set; }

        public IEnumerable<Checkout> Checkouts { get; set; }
        public IEnumerable<CheckoutHistory> CheckoutHistory { get; set; }
        public IEnumerable<Checkin> Checkins { get; set; }
    }
}
