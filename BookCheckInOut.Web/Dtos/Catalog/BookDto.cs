using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCheckInOut.Web.Dtos.Catalog
{
    public class BookDto
    {
        public IQueryable<BookListDto> Books { get; set; }
    }
}
