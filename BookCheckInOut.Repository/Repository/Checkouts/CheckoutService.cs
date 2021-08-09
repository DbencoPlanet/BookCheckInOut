using BookCheckInOut.Core.Checkins;
using BookCheckInOut.Core.Checkouts;
using BookCheckInOut.Core.Enums;
using BookCheckInOut.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCheckInOut.Repository.Repository.Checkouts
{
    public class CheckoutService : ICheckoutService
    {
        private readonly BookCheckInOutDBContext _context;

        public CheckoutService(BookCheckInOutDBContext context)
        {
            _context = context;
        }

        public void Add(Checkout newCheckout)
        {
            _context.Add(newCheckout);
            _context.SaveChanges();
        }


        public IEnumerable<Checkout> GetAll()
        {
            return _context.Checkouts;
        }

        public Checkout GetById(long checkoutId)
        {
            return GetAll()
                .FirstOrDefault(checkout => checkout.Id == checkoutId);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(long id)
        {
            return _context.CheckoutHistories
                .Include(a => a.Book)
                .Include(a => a.Person)
                .Where(a => a.BookId == id);
        }


        public IEnumerable<Checkin> GetCurrentCheckin(long id)
        {
            return _context.Checkins
                 .Include(h => h.Book)
                 .Include(h => h.Person)
                 .Where(a => a.BookId == id);
        }

        public Checkout GetLatestCheckout(long id)
        {
            return _context.Checkouts
                .Where(c => c.BookId == id)
                .OrderByDescending(c => c.CheckoutDate)
                .FirstOrDefault();

        }

        public void MarkFound(long bookId)
        {
            var now = DateTime.Now;

            UpdateBookStatus(bookId, BookStatus.Checkin);
            RemoveExistingCheckouts(bookId);

            CloseExistingCheckoutHistories(bookId, now);

            _context.SaveChanges();
        }

        private void UpdateBookStatus(long bookId, BookStatus bookStatus)
        {
            var item = _context.Books
                            .FirstOrDefault(a => a.Id == bookId);
            item.Status = bookStatus;
            _context.Update(item);

          
        }

        private void CloseExistingCheckoutHistories(long bookId, DateTime now)
        {
            //close existing checkout history

            var history = _context.CheckoutHistories
                .FirstOrDefault(h => h.BookId == bookId && h.CheckedIn == null);

            if (history != null)
            {
                history.CheckedIn = now;
                _context.Update(history);

            }
        }

        private void RemoveExistingCheckouts(long bookId)
        {
            // remove existing checkouts on item
            var checkout = _context.Checkouts
                .FirstOrDefault(co => co.BookId == bookId);

            if (checkout != null)
            {
                _context.Remove(checkout);
            }
        }

        public void MarkLost(long bookId)
        {
            UpdateBookStatus(bookId, BookStatus.Lost);

            _context.SaveChanges();
        }

        public void Checkin(long bookId, long personId)
        {
            var now = DateTime.Now;
            var book = _context.Books
                .FirstOrDefault(a => a.Id == bookId);
            var person = _context.Persons.FirstOrDefault(c => c.Id == personId);

            if (book.Status == BookStatus.Checkin)
            {
                UpdateBookStatus(bookId, BookStatus.Checkin);
            }

            var checkin = new Checkin
            {
                CheckedinDate = now,
                BookId = bookId,
                PersonId = personId
            };
            _context.Add(checkin);
            _context.SaveChanges();
        }

        public void CheckInItem(long bookId)
        {
            var now = DateTime.Now;

            var item = _context.Books
                .FirstOrDefault(a => a.Id == bookId);

            // remove existing checkouts
            RemoveExistingCheckouts(bookId);
            // close existing checkout history
            CloseExistingCheckoutHistories(bookId, now);
            // look for existing holds
            var currentHolds = _context.Checkins
                .Include(h => h.Person)
                .Include(h => h.Book)
                .Where(h => h.BookId == bookId);
            // if there are holds, checkout item to the library card with earliest hold
            if (currentHolds.Any())
            {
                CheckoutToEarliestHold(bookId, currentHolds);
                return;
            }
            // otherwise update the item status to available
            UpdateBookStatus(bookId, BookStatus.Checkin);

           
            _context.SaveChanges();

        }

        private void CheckoutToEarliestHold(long bookId, IQueryable<Checkin> currentHolds)
        {
            var earliestHolds = currentHolds
                .OrderBy(holds => holds.CheckedinDate)
                .FirstOrDefault();

            var card = earliestHolds.Person;

            _context.Remove(earliestHolds);
            _context.SaveChanges();

            CheckOutItem(bookId, card.Id);
        }

        public void CheckOutItem(long bookId, long personId)
        {
            if (IsCheckedOut(bookId))
            {
                return;
                // add logic to handle feedback to user
            }

            var item = _context.Books
                .FirstOrDefault(a => a.Id == bookId);

            UpdateBookStatus(bookId, BookStatus.Checkout);
            var person = _context.Persons
                .Include(p => p.Checkouts)
                .FirstOrDefault(p => p.Id == personId);

            var now = DateTime.Now;
            var checkout = new Checkout
            {
                BookId = item.Id,
                PersonId = personId,
                CheckoutDate = now,
                ReturnDate = GetDefaultCheckoutTime(now)
            };

            _context.Add(checkout);
            var checkoutHistory = new CheckoutHistory
            {
                CheckedOut = now,
                BookId = bookId,
                PersonId = personId
            };

            _context.Add(checkoutHistory);
            _context.SaveChanges();
        }

        private DateTime GetDefaultCheckoutTime(DateTime now)
        {
            //return now.AddDays(15);
            return GetNextWorkingDay(now);
        }

        public bool IsCheckedOut(long bookId)
        {
            return _context.Checkouts
                .Where(co => co.BookId == bookId)
                .Any();

        }

        public string GetCurrentCheckinPersonName(long id)
        {
            var hold = _context.Checkins
                .Include(h => h.Book)
                .Include(h => h.Person).
                FirstOrDefault(h => h.PersonId == id);

            var personId = hold?.PersonId;

            var person = _context.Persons
                .FirstOrDefault(p => p.Id == personId);
            return person?.FirstName + " " + person?.LastName;
        }

        public DateTime GetCurrentCheckedin(long id)
        {
            return _context.Checkins
                .Include(h => h.Person)
                .Include(h => h.Book)
                .FirstOrDefault(h => h.Id == id)
                .CheckedinDate;


        }

        public string GetCurrentCheckedoutPerson(long bookId)
        {
            var checkout = GetCheckoutByBookId(bookId);

            if (checkout == null)
            {
                return "";
            };

            var personId = checkout.PersonId;

            var person = _context.Persons
                .FirstOrDefault(p => p.Id == personId);
            return person.FirstName + " " + person.LastName;

        }

        private Checkout GetCheckoutByBookId(long bookId)
        {
            return _context.Checkouts
                .Include(co => co.Book)
                .Include(co => co.Person)
                .FirstOrDefault(co => co.BookId == bookId);
        }

        private static bool IsHoliday(DateTime date)
        {
            var Holidays = new List<DateTime>();
            Holidays.Add(new DateTime(DateTime.Now.Year, 1, 1));
            Holidays.Add(new DateTime(DateTime.Now.Year, 1, 5));
            Holidays.Add(new DateTime(DateTime.Now.Year, 3, 10));
            Holidays.Add(new DateTime(DateTime.Now.Year, 5, 27));
            Holidays.Add(new DateTime(DateTime.Now.Year, 1, 10));
            Holidays.Add(new DateTime(DateTime.Now.Year, 12, 25));

            return Holidays.Contains(date);
        }

        private static bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday
                || date.DayOfWeek == DayOfWeek.Sunday;
        }


        private static DateTime GetNextWorkingDay(DateTime date)
        {
            do
            {
                date = date.AddDays(15);
            } while (IsHoliday(date) || IsWeekend(date));
            return date;
        }
    }
}
