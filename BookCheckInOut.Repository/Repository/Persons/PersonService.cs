using BookCheckInOut.Core.Checkins;
using BookCheckInOut.Core.Checkouts;
using BookCheckInOut.Core.Persons;
using BookCheckInOut.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCheckInOut.Repository.Repository.Persons
{
    public class PersonService : IPersonService
    {
        private readonly BookCheckInOutDBContext _context;

        public PersonService(BookCheckInOutDBContext context)
        {
            _context = context;
        }

        public Person Add(Person newPerson)
        {
            //newPerson.PenaltyFee = 500;
            _context.Add(newPerson);
            _context.SaveChanges();
            return newPerson;
        }

        public Person Edit(Person EditPerson)
        {
            //newPerson.PenaltyFee = 500;
            _context.Entry(EditPerson).State = EntityState.Modified;
            _context.SaveChanges();
            return EditPerson;
        }

        public Person Get(long id)
        {
            return GetAll()
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Person> GetAll()
        {
            return _context.Persons
                .Include(p => p.Checkouts);
        }

        public Person GetById(long personId)
        {
            return GetAll()
                .FirstOrDefault(p => p.Id == personId);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(long personId)
        {

            return _context.CheckoutHistories
                .Include(co => co.Person)
                .Include(co => co.Book)
                .Where(co => co.PersonId == personId)
                .OrderByDescending(co => co.CheckedOut);

        }

        public IEnumerable<Checkout> GetCheckouts(long personId)
        {
           

            return _context.Checkouts
                .Include(co => co.Person)
                .Include(co => co.Book)
                .Where(co => co.PersonId == personId);
        }

        public IEnumerable<Checkin> GetCheckins(long personId)
        {

            return _context.Checkins
                .Include(h => h.Person)
                .Include(h => h.Book)
                .Where(h => h.PersonId == personId)
                .OrderByDescending(h => h.CheckedinDate);
        }
    }
}
