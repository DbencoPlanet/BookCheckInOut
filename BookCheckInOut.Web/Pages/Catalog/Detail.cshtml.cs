using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCheckInOut.Repository.Repository.Books;
using BookCheckInOut.Repository.Repository.Checkouts;
using BookCheckInOut.Web.Dtos.Catalog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BookCheckInOut.Web.Pages.Catalog
{
    public class DetailModel : PageModel
    {
        private readonly IBookService _bookService;
        private readonly ICheckoutService _checkoutService;
        private readonly ILogger<DetailModel> _logger;

        public DetailModel(
            IBookService bookService,
            ICheckoutService checkoutService,
            ILogger<DetailModel> logger
            )
        {
            _bookService = bookService;
            _checkoutService = checkoutService;
            _logger = logger;
        }

        [BindProperty]
        public BookDetailDto Book { get; set; }
        public void OnGet(long id)
        {
            var getBook = _bookService.GetById(id);
            var currentCheckedin = _checkoutService.GetCurrentCheckin(id);
            currentCheckedin.Select(a => new BookCheckinModel
            {
                Checkedin = _checkoutService.GetCurrentCheckedin(a.Id).ToString("d"),
                PersonName = _checkoutService.GetCurrentCheckinPersonName(a.Id)
            });
            var bookinfo = new BookDetailDto
            {
                Id = id,
                Cost = getBook.Cost,
                ImageUrl = getBook.ImageUrl,
                ISBN = getBook.ISBN,
                Status = getBook.Status,
                Title = getBook.Title,
                Year = getBook.Year,
                CheckoutHistory = _checkoutService.GetCheckoutHistory(id),
                LastestCheckout = _checkoutService.GetLatestCheckout(id),
                PersonName = _checkoutService.GetCurrentCheckedoutPerson(id),
                CurrentCheckin = currentCheckedin

            };
            Book = bookinfo;
        }

        public IActionResult OnPostCheckIn(long id)
        {

            try
            {

                _checkoutService.CheckInItem(id);
                _logger.LogInformation("Checked in item successfully");
                return RedirectToPage("./Detail", new { id = id });
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Checked in item not successful");
                throw ex;

            }
        }

        public IActionResult OnPostMarkLost(long id)
        {

            try
            {

                _checkoutService.MarkLost(id);
                _logger.LogInformation("item has been marked successfully!");
                return RedirectToPage("./Detail", new { id = id });
            }
            catch (Exception ex)
            {
                _logger.LogInformation("item was not marked!");
                throw ex;

            }
        }

        public IActionResult OnPostMarkFound(long id)
        {

            try
            {

                _checkoutService.MarkFound(id);
                _logger.LogInformation("item has been marked successfully!");
                return RedirectToPage("./Detail", new { id = id });
            }
            catch (Exception ex)
            {
                _logger.LogInformation("item was not marked!");
                throw ex;

            }
        }
    }
}
