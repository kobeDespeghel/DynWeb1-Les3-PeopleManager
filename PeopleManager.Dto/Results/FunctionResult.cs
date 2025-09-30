using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManager.Dto.Results
{
    public class FunctionResult
    {
        public int Id { get; set; }
        
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int NumberOfPeople { get; set; }
    }
}
