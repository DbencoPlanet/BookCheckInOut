using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookCheckInOut.Core.Books;
using BookCheckInOut.Repository.Repository.Books;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;


namespace BookCheckInOut.Web.Pages.Books
{
    public class AddModel : PageModel
    {
        private readonly IBookService _bookService;
        private readonly ILogger<AddModel> _logger;
        private readonly IHostingEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddModel(
            IBookService bookService,
            ILogger<AddModel> logger,
            IHostingEnvironment env,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _bookService = bookService;
            _logger = logger;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public Book Book { get; set; }

        public IFormFile file { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {

            try
            {
                if (file != null)
                {
                    var results = "";

                    var webRoot = _env.WebRootPath.Trim();
                    var webUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}";
                    var uploads = Path.Combine(webRoot, "Uploads");
                    var extension = "";
                    var filePath = "";
                    var fileName = "";
                    var uniqueFileName = "";

                    extension = Path.GetExtension(file.FileName);
                    uniqueFileName = Guid.NewGuid().ToString();

                    uniqueFileName = uniqueFileName.Replace(",", " ")
                        .Replace(".", " ").Replace("+", " ").Replace("-", " ")
                        .Replace(";", " ").Replace(":", " ").Replace("/", " ")
                        .Replace(">", " ").Replace("<", " ").Replace("?", " ")
                        .Replace("\"", " ").Replace("'", " ").Replace("|", " ")
                        .Replace("%", " ");
                    uniqueFileName = uniqueFileName.Replace(" ", "-").Replace("--", "-").Replace("-", "-");
                    uniqueFileName = uniqueFileName.Replace(" ", "-").Replace("--", "-").Replace("-", "-");
                    fileName = uniqueFileName + extension;
                    filePath = Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    results = fileName;
                    Book.ImageUrl = webUrl + "/Uploads/" + fileName;
                }
                else
                {
                    Book.ImageUrl = Book.ImageUrl;
                }
                Book.Status = Core.Enums.BookStatus.Checkin;
                var book = _bookService.Add(Book);
                _logger.LogInformation("Book item added successfully");
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
