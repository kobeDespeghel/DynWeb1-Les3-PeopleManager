using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PeopleManager.Model;

namespace PeopleManager.Repository
{
    public class PeopleManagerDbContext(DbContextOptions<PeopleManagerDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Function> Functions => Set<Function>();
        public DbSet<Person> People => Set<Person>();
        
        public void Seed()
        {
            AddDefaultIdentityUser();

            var developerFunction = new Function
            {
                Name = "Developer"
            };
            Functions.Add(developerFunction);

            People.AddRange(new List<Person>
                {
                    new Person { FirstName = "John", LastName = "Smith", Email = "john.smith@example.com", Function = developerFunction},
                    new Person { FirstName = "Jane", LastName = "Doe" }, // no Email
                    new Person { FirstName = "Emily", LastName = "Jones", Email = "emily.jones@example.com" },
                    new Person { FirstName = "Michael", LastName = "Brown", Function = developerFunction}, // no Email
                    new Person { FirstName = "Sarah", LastName = "Johnson" }, // no Email
                    new Person { FirstName = "Thomas", LastName = "Williams", Email = "thomas.williams@example.com" },
                    new Person { FirstName = "Amanda", LastName = "Miller", Function = developerFunction}, // no Email
                    new Person { FirstName = "Daniel", LastName = "Davis", Email = "daniel.davis@example.com" },
                    new Person { FirstName = "Laura", LastName = "Wilson" }, // no Email
                    new Person { FirstName = "Peter", LastName = "Taylor", Email = "peter.taylor@example.com" }
                });

            SaveChanges();
        }

        private void AddDefaultIdentityUser()
        {
            var email = "bavo.ketels@vives.be";

            var identityUser = new IdentityUser();
            identityUser.UserName = email;
            identityUser.NormalizedUserName = email.ToUpper();
            identityUser.Email = email;
            identityUser.NormalizedEmail = email.ToUpper();
            identityUser.PasswordHash = "AQAAAAIAAYagAAAAEJb8pZkj/vdlh+joOkzrHJhUYXu+6JoLwiqL+Cs1OkviTiQKtM/dkPD2TcZ/JFUtPg=="; //Test123$

            Users.Add(identityUser);
        }
    }
}
