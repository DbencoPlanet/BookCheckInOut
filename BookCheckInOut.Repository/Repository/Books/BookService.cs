using BookCheckInOut.Core.Books;
using BookCheckInOut.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCheckInOut.Repository.Repository.Books
{
    public class BookService : IBookService
    {
        private readonly BookCheckInOutDBContext _context;
        public BookService(BookCheckInOutDBContext context)
        {
            _context = context;
        }

        public Book Add(Book newBook)
        {
            _context.Add(newBook);
            _context.SaveChanges();
            return newBook;

        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books;
        }

        public Book GetById(long id)
        {
            return _context.Books
                .FirstOrDefault(b => b.Id == id);
        }

      
        public int GetYear(long id)
        {
            if (_context.Books.Any(book => book.Id == id))
            {
                return _context.Books
                    .FirstOrDefault(book => book.Id == id)
                    .Year;
            }
            else return 0;
        }

        public string GetIsbn(long id)
        {
            if (_context.Books.Any(a => a.Id == id))
            {
                return _context.Books
                    .FirstOrDefault(a => a.Id == id)
                    .ISBN;
            }
            else return "";
        }

        public string GetTitle(long id)
        {
            return _context.Books
                .FirstOrDefault(title => title.Id == id)
                .Title;
        }

        public decimal GetCost(long id)
        {
            return _context.Books
                 .FirstOrDefault(title => title.Id == id)
                 .Cost;
        }

        public string GetImage(long id)
        {
            return _context.Books
                .FirstOrDefault(title => title.Id == id)
                .ImageUrl;
        }
    }
}
