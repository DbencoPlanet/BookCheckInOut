using BookCheckInOut.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCheckInOut.Web.Dtos.Catalog
{
    public class BookListDto
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string ISBN { get; set; }

        public int Year { get; set; }

        public decimal Cost { get; set; }

        public string ImageUrl { get; set; }

        public BookStatus Status { get; set; }
    }
}
