using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCheckInOut.Repository.Repository.Persons;
using BookCheckInOut.Web.Dtos.Persons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookCheckInOut.Web.Pages.Persons
{
    public class IndexModel : PageModel
    {
        private readonly IPersonService _personService;

        public IndexModel(
            IPersonService personService
            )
        {
            _personService = personService;
        }

        [BindProperty]
        public IQueryable<PersonDetailDto> Person { get; set; }
        public void OnGet()
        {

            List<PersonDetailDto> data = new List<PersonDetailDto>();
            var getBook = _personService.GetAll();
            data.AddRange(getBook.Select(result => new PersonDetailDto
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                PenaltyFee = result.PenaltyFee,
                MobileNo = result.MobileNo,
                NationalID = result.NationalID
               
            }));

            Person = data.AsQueryable();
        }
    }
}
