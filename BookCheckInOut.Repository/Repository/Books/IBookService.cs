using BookCheckInOut.Core.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCheckInOut.Repository.Repository.Books
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book GetById(long id);

        Book Add(Book newBook);
        int GetYear(long id);
        decimal GetCost(long id);
        string GetImage(long id);
        string GetTitle(long id);
        string GetIsbn(long id);

    }
}
