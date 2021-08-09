using BookCheckInOut.Core.Books;
using BookCheckInOut.Core.Persons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCheckInOut.Core.Checkouts
{
    public class CheckoutHistory
    {
        public long Id { get; set; }

        public long? BookId { get; set; }
        public Book Book { get; set; }

        public long? PersonId { get; set; }
        public Person Person { get; set; }

        public decimal? PenaltyFee { get; set; }

        public DateTime CheckedOut { get; set; }

        public DateTime? CheckedIn { get; set; }
    }
}
