using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCheckInOut.Core.Checkouts;
using BookCheckInOut.Core.Persons;
using BookCheckInOut.Repository.Repository.Books;
using BookCheckInOut.Repository.Repository.Checkouts;
using BookCheckInOut.Repository.Repository.Persons;
using BookCheckInOut.Web.Dtos.Catalog;
using BookCheckInOut.Web.Dtos.Checkouts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BookCheckInOut.Web.Pages.Catalog
{
    public class CheckoutModel : PageModel
    {
        private readonly IBookService _bookService;
        private readonly ICheckoutService _checkoutService;
        private readonly ILogger<CheckoutModel> _logger;
        private readonly IPersonService _personService;

        public CheckoutModel(
            IBookService bookService,
            ICheckoutService checkoutService,
            ILogger<CheckoutModel> logger,
            IPersonService personService
            )
        {
            _bookService = bookService;
            _checkoutService = checkoutService;
            _logger = logger;
            _personService = personService;
        }

        [BindProperty]
        public CheckoutDto Checkout { get; set; }

        [BindProperty]
        public BookDetailDto Book { get; set; }

        [BindProperty]
        public Person Person { get; set; }

        [BindProperty]
        public long BookId { get; set; }

        public void OnGet(long id)
        {
            var getBook = _bookService.GetById(id);
            Book = new BookDetailDto
            {
                Id = getBook.Id,
                Cost = getBook.Cost,
                ImageUrl = getBook.ImageUrl,
                ISBN = getBook.ISBN,
                Year = getBook.Year,
                Status = getBook.Status,
                Title = getBook.Title
            };

            Checkout = new CheckoutDto
            {
                BookId = id,
                ImageUrl = getBook.ImageUrl,
                PersonId = null,
                IsCheckedOut = _checkoutService.IsCheckedOut(id)
            };
        }

        public IActionResult OnPost(long id)
        {

            try
            {
                var person = _personService.Add(Person);
                _checkoutService.CheckOutItem(BookId,person.Id);
                _logger.LogInformation("Checked out item successfully");
                return RedirectToPage("./Detail", new { id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
