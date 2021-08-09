using BookCheckInOut.Core.Checkins;
using BookCheckInOut.Core.Checkouts;
using BookCheckInOut.Core.Persons;
using BookCheckInOut.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCheckInOut.Web.Dtos.Catalog
{
    public class BookDetailDto
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string ISBN { get; set; }

        public int Year { get; set; }

        public decimal Cost { get; set; }

        public string ImageUrl { get; set; }

        public string PersonName { get; set; }

        //public string Mobile { get; set; }

        //public string NationalID { get; set; }
        public DateTime CheckoutDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public BookStatus Status { get; set; }

        public Checkout LastestCheckout { get; set; }
        public Person CurrentAssociatedPerson { get; set; }
        public IEnumerable<CheckoutHistory> CheckoutHistory { get; set; }
        public IEnumerable<Checkin> CurrentCheckin { get; set; }
    }

    public class BookCheckinModel
    {
        public string PersonName { get; set; }
        public string Checkedin { get; set; }
    }
}
