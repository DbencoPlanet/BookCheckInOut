using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCheckInOut.Web.Dtos.Persons
{
    public class PersonDto
    {
        public IEnumerable<PersonDetailDto> Persons { get; set; }
    }
}
