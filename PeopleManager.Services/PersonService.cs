using Microsoft.EntityFrameworkCore;
using PeopleManager.Model;
using PeopleManager.Repository;

namespace PeopleManager.Services
{
    public class PersonService
    {
        private readonly PeopleManagerDbContext _dbContext;

        public PersonService(PeopleManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Person> Find()
        {
            var people = _dbContext.People
                .Include(p => p.Function)
                .ToList();
            return people;
        }

        public Person? Get(int id)
        {
            var person = _dbContext.People
                .Include(p => p.Function)
                .FirstOrDefault(p => p.Id == id);
            return person;
        }

        public Person? Create(Person person)
        {
            if (string.IsNullOrWhiteSpace(person.FirstName))
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(person.LastName))
            {
                return null;
            }

            _dbContext.People.Add(person);

            _dbContext.SaveChanges();

            return person;
        }


        public Person? Update(int id, Person person)
        {
            var dbPerson = Get(id);

            if (dbPerson == null)
            {
                return null;
            }

            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Email = person.Email;
            dbPerson.FunctionId = person.FunctionId;

            _dbContext.SaveChanges();

            return dbPerson;
        }

        public void Delete(int id)
        {
            var person = Get(id);

            if (person is null)
            {
                return;
            }
            //var person = new Person { Id = id, FirstName = string.Empty, LastName = string.Empty };
            //_dbContext.People.Attach(person);

            _dbContext.People.Remove(person);

            _dbContext.SaveChanges();
        }
    }
}
