using BookCheckInOut.Core.Checkins;
using BookCheckInOut.Core.Checkouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCheckInOut.Repository.Repository.Checkouts
{
    public interface ICheckoutService
    {
        void Add(Checkout newCheckout);

        IEnumerable<Checkout> GetAll();
        IEnumerable<CheckoutHistory> GetCheckoutHistory(long id);
        IEnumerable<Checkin> GetCurrentCheckin(long id);

        Checkout GetById(long checkoutId);
        Checkout GetLatestCheckout(long bookId);
        string GetCurrentCheckedoutPerson(long bookId);
        string GetCurrentCheckinPersonName(long id);
        DateTime GetCurrentCheckedin(long id);
        bool IsCheckedOut(long id);

        void CheckOutItem(long bookId, long personId);
        void CheckInItem(long bookId);
        void Checkin(long bookId, long personId);
        void MarkLost(long bookId);
        void MarkFound(long bookId);
    }
}
