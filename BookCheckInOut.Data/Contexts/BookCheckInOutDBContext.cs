using BookCheckInOut.Core.Books;
using BookCheckInOut.Core.Checkins;
using BookCheckInOut.Core.Checkouts;
using BookCheckInOut.Core.Persons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCheckInOut.Data.Contexts
{
    public class BookCheckInOutDBContext: DbContext
    {
        public BookCheckInOutDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Checkin> Checkins { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<CheckoutHistory> CheckoutHistories { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}
