using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCheckInOut.Repository.Repository.Books;
using BookCheckInOut.Repository.Repository.Checkouts;
using BookCheckInOut.Web.Dtos.Catalog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookCheckInOut.Web.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly IBookService _bookService;
        private readonly ICheckoutService _checkoutService;

        public IndexModel(
            IBookService bookService,
            ICheckoutService checkoutService
            )
        {
            _bookService = bookService;
            _checkoutService = checkoutService;
        }

        [BindProperty]
        public IQueryable<BookListDto> Books { get; set; }
        public void OnGet()
        {

            List<BookListDto> data = new List<BookListDto>();
            var getBook = _bookService.GetAll();
            data.AddRange(getBook.Select(result => new BookListDto
            {
                Id = result.Id,
                ImageUrl = result.ImageUrl,
                Title = result.Title,
                ISBN = result.ISBN,
                Year = result.Year,
                Cost = result.Cost,
                Status = result.Status
            }));

            Books = data.AsQueryable();

            
        }
    }
}
