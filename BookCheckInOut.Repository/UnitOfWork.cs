using BookCheckInOut.Data.Contexts;
using BookCheckInOut.Repository.Repository.Books;
using BookCheckInOut.Repository.Repository.Checkouts;
using BookCheckInOut.Repository.Repository.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCheckInOut.Repository
{
    public class UnitOfWork : IDisposable
    {
        private readonly BookCheckInOutDBContext _context;
        public readonly ICheckoutService CheckoutService;
        public readonly IPersonService PersonService;
        public readonly IBookService BookService;


        public UnitOfWork(
            BookCheckInOutDBContext context,
            ICheckoutService checkoutService,
            IPersonService personService,
            IBookService bookService
            )
        {
            _context = context;
            CheckoutService = checkoutService;
            PersonService = personService;
            BookService = bookService;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        #region IDisposable Support
        public bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }
        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }


        #endregion
    }
}
