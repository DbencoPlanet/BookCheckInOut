using BookCheckInOut.Core.Checkins;
using BookCheckInOut.Core.Checkouts;
using BookCheckInOut.Core.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCheckInOut.Repository.Repository.Persons
{
    public interface IPersonService
    {
        Person Get(long id);
        IEnumerable<Person> GetAll();
        Person Add(Person newPerson);

        Person Edit(Person EditPerson);

        Person GetById(long personId);

        IEnumerable<CheckoutHistory> GetCheckoutHistory(long personId);
        IEnumerable<Checkin> GetCheckins(long personId);
        IEnumerable<Checkout> GetCheckouts(long patronId);
    }
}
