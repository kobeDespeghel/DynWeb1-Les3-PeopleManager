using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManager.Dto.Filters
{
    public class PersonFilter
    {
        public string? Search { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public bool UseEmailFilter { get; set; }
        public string? Email { get; set; }

        public bool UseFunctionFilter { get; set; }
        public int FunctionId { get; set; }

        public string? FunctionName { get; set; }

    }
}
