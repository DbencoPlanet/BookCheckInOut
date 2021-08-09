using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCheckInOut.Web.Dtos.Checkouts
{
    public class CheckoutDetailDto
    {
        public long BookId { get; set; }
        public string PersonName { get; set; }
        public string MobileNo { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public string ImageUrl { get; set; }

    }
}
