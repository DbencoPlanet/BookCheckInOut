using BookCheckInOut.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCheckInOut.Core.Books
{
    public class Book
    {
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public decimal Cost { get; set; }

        public string ImageUrl { get; set; }

        public BookStatus Status { get; set; }

    }
}
