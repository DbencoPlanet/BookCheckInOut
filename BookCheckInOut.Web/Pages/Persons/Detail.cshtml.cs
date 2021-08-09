using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCheckInOut.Core.Checkouts;
using BookCheckInOut.Repository.Repository.Persons;
using BookCheckInOut.Web.Dtos.Persons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookCheckInOut.Web.Pages.Persons
{
    public class DetailModel : PageModel
    {
        private readonly IPersonService _personService;

        public DetailModel(
            IPersonService personService
            )
        {
            _personService = personService;
        }

        [BindProperty]
        public PersonDetailDto Person { get; set; }
        public void OnGet(long id)
        {
            var person = _personService.Get(id);

            var model = new PersonDetailDto
            {
                LastName = person.LastName,
                FirstName = person.FirstName,
                MobileNo = person.MobileNo,
                NationalID = person.NationalID,
                PenaltyFee = person.PenaltyFee,
                Checkouts = _personService.GetCheckouts(id).ToList() ?? new List<Checkout>(),
                CheckoutHistory = _personService.GetCheckoutHistory(id),
                Checkins = _personService.GetCheckins(id)
            };
            Person = model;
        }
    }
}
