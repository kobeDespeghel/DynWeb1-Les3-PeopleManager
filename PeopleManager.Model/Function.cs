using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeopleManager.Model
{
    [Table(nameof(Function))]
    public class Function
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }
        public string? Description { get; set; }

        public IList<Person> People { get; set; } = new List<Person>();
    }
}
