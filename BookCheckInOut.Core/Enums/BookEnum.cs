using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCheckInOut.Core.Enums
{
    public enum BookStatus
    {
        [Description("Check in")]
        [Display(Name = "Check in")]
        Checkin = 1,

        [Description("Check out")]
        [Display(Name = "Check out")]
        Checkout = 2,

        [Description("Lost")]
        [Display(Name = "Lost")]
        Lost = 3

    }
}
