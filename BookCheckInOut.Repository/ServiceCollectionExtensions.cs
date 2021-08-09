using BookCheckInOut.Repository.Repository.Books;
using BookCheckInOut.Repository.Repository.Checkouts;
using BookCheckInOut.Repository.Repository.Persons;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCheckInOut.Repository
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<ICheckoutService, CheckoutService>();
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<UnitOfWork>();
            return services;
        }
    }
}
