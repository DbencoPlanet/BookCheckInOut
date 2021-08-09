using BookCheckInOut.Core.Checkouts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCheckInOut.Core.Persons
{
    public class Person
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

        public virtual IEnumerable<Checkout> Checkouts { get; set; }
    }
}
