using Microsoft.EntityFrameworkCore;
using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
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

        public async Task<IList<PersonResult>> Get()
        {
            var people = await _dbContext.People
                .Include(p => p.Function)
                .Select(p => new PersonResult
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    FunctionId = p.FunctionId,
                    FunctionName = p.Function == null ? null : p.Function.Name,
                })
                .ToListAsync();
            return people;
        }

        public async Task<PersonResult?> GetById(int id)
        {
            var person = await _dbContext.People
                .Include(p => p.Function)
                .Select(p => new PersonResult
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    FunctionId = p.FunctionId,
                    FunctionName = p.Function == null ? null : p.Function.Name,
                })
                .FirstOrDefaultAsync(p => p.Id == id);
            return person;
        }

        public async Task<PersonResult?> Create(PersonRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName))
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(request.LastName))
            {
                return null;
            }

            var newPerson = new Person
            {
               FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                FunctionId = request.FunctionId
            };

            _dbContext.People.Add(newPerson);

            await _dbContext.SaveChangesAsync();

            return await GetById(newPerson.Id);
        }


        public async Task<PersonResult?> Update(int id, PersonRequest request)
        {
            var person = await _dbContext.People
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
            {
                return null;
            }

            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.Email = request.Email;
            person.FunctionId = request.FunctionId;

            await _dbContext.SaveChangesAsync();

            return await GetById(person.Id);
        }

        public async Task Delete(int id)
        {
            var person = await _dbContext.People
                .FirstOrDefaultAsync(p => p.Id == id);

            //var person = new Person { Id = id, FirstName = string.Empty, LastName = string.Empty };
            //_dbContext.People.Attach(person);

            if (person is null)
            {
                return;
            }

            _dbContext.People.Remove(person);

            await _dbContext.SaveChangesAsync();
        }
    }
}
