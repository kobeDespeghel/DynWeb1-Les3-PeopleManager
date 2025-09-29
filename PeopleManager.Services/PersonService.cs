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

        public async Task<IList<Person>> Get()
        {
            var people = await _dbContext.People
                .Include(p => p.Function)
                .ToListAsync();
            return people;
        }

        public async Task<Person?> GetById(int id)
        {
            var person = await _dbContext.People
                .Include(p => p.Function)
                .FirstOrDefaultAsync(p => p.Id == id);
            return person;
        }

        public async Task<Person?> Create(Person person)
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

            await _dbContext.SaveChangesAsync();

            return person;
        }


        public async Task<Person?> Update(int id, Person person)
        {
            var dbPerson = await GetById(id);

            if (dbPerson == null)
            {
                return null;
            }

            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Email = person.Email;
            dbPerson.FunctionId = person.FunctionId;

            await _dbContext.SaveChangesAsync();

            return dbPerson;
        }

        public async Task Delete(int id)
        {
            var person = await GetById(id);

            if (person is null)
            {
                return;
            }
            //var person = new Person { Id = id, FirstName = string.Empty, LastName = string.Empty };
            //_dbContext.People.Attach(person);

            _dbContext.People.Remove(person);

            await _dbContext.SaveChangesAsync();
        }
    }
}
