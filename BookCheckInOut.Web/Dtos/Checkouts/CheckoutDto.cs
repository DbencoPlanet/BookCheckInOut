using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCheckInOut.Web.Dtos.Checkouts
{
    public class CheckoutDto
    {
        public long? PersonId { get; set; }
        public string Title { get; set; }
        public long BookId { get; set; }
        public string ImageUrl { get; set; }
        public int CheckinCount { get; set; }
        public bool IsCheckedOut { get; set; }
    }
}
