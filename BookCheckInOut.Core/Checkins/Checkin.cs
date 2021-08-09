using BookCheckInOut.Core.Books;
using BookCheckInOut.Core.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCheckInOut.Core.Checkins
{
    public class Checkin
    {
        public long Id { get; set; }
        public long? BookId { get; set; }
        public Book Book { get; set; }

        public long? PersonId { get; set; }
        public Person Person { get; set; }

        public DateTime CheckedinDate { get; set; }
    }
}
