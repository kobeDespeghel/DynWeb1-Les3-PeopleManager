using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PeopleManager.Model
{
    [Table(nameof(Person))]
    public class Person
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Email { get; set; }
        public int? FunctionId { get; set; }
        public Function? Function { get; set; }
    }
}
