using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCheckInOut.Core.Persons;
using BookCheckInOut.Repository.Repository.Books;
using BookCheckInOut.Repository.Repository.Checkouts;
using BookCheckInOut.Repository.Repository.Persons;
using BookCheckInOut.Web.Dtos.Catalog;
using BookCheckInOut.Web.Dtos.Checkouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BookCheckInOut.Web.Pages.Catalog
{
    public class CheckinModel : PageModel
    {
        private readonly IBookService _bookService;
        private readonly ICheckoutService _checkoutService;
        private readonly ILogger<CheckinModel> _logger;
        private readonly IPersonService _personService;

        public CheckinModel(
            IBookService bookService,
            ICheckoutService checkoutService,
            ILogger<CheckinModel> logger,
            IPersonService personService
            )
        {
            _bookService = bookService;
            _checkoutService = checkoutService;
            _logger = logger;
            _personService = personService;
        }

        [BindProperty]
        public CheckoutDetailDto Checkout { get; set; }

        [BindProperty]
        public long BookId { get; set; }

        [BindProperty]
        public Person Person { get; set; }

        public void OnGet(long id)
        {
            var getCheckout = _checkoutService.GetById(id);
            var getPerson = _personService.GetById(getCheckout.PersonId.GetValueOrDefault());
            var getBook = _bookService.GetById(getCheckout.BookId.GetValueOrDefault());
            var now = getCheckout.ReturnDate;
            Checkout = new CheckoutDetailDto
            {
                BookId = getBook.Id,
                PersonName = getPerson.FullName,
                MobileNo = getPerson.MobileNo,
                ReturnDate = getCheckout.ReturnDate,
                RequiredDate = GetDefaultCheckoutTime(now),
                ImageUrl = getBook.ImageUrl
            };
        }

        public IActionResult OnPost(long id)
        {

            try
            {
                var getCheckout = _checkoutService.GetById(id);
                var getPerson = _personService.GetById(getCheckout.PersonId.GetValueOrDefault());
                var returnDate = getCheckout.ReturnDate;
                var now = DateTime.Now;
                _checkoutService.CheckInItem(id);
                if(now > GetDefaultCheckoutTime(returnDate))
                {
                    Person.Id = getPerson.Id;
                    Person.FirstName = getPerson.FirstName;
                    Person.LastName = getPerson.LastName;
                    Person.NationalID = getPerson.NationalID;
                    Person.MobileNo = getPerson.MobileNo;
                    Person.PenaltyFee = 500;
                    _personService.Edit(Person);
                }
                _logger.LogInformation("Checked in item successfully");
                return RedirectToPage("./Detail", new { id = id });
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Checked in item not successful");
                throw ex;

            }
        }

        private DateTime GetDefaultCheckoutTime(DateTime now)
        {
            //return now.AddDays(15);
            return GetNextWorkingDay(now);
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
