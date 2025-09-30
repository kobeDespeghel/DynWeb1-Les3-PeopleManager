using System.ComponentModel.DataAnnotations;

namespace PeopleManager.Dto.Requests
{
    public class FunctionRequest
    {
        [Required]
        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}
