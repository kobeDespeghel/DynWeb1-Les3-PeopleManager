using Microsoft.EntityFrameworkCore;
using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using PeopleManager.Model;
using PeopleManager.Repository;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;

namespace PeopleManager.Services
{
    public class PersonService
    {
        private readonly PeopleManagerDbContext _dbContext;

        public PersonService(PeopleManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceResult<IList<PersonResult>>> Get(string? sorting)
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
                .OrderBy(sorting)
                .ToListAsync();
            //return people;
            return new ServiceResult<IList<PersonResult>>()
            {
                Data = people
            };
        }

        public async Task<ServiceResult<PersonResult>> GetById(int id)
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

            if(person == null)
            {
                return new ServiceResult<PersonResult>().NotFound(entityName: "Person");
            }

            //return person;
            return new ServiceResult<PersonResult>()
            {
                Data = person
            };
        }

        public async Task<ServiceResult<PersonResult>> Create(PersonRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName))
            {
                return new ServiceResult<PersonResult>().Required(nameof(request.FirstName));
            }
            if (string.IsNullOrWhiteSpace(request.LastName))
            {
                return new ServiceResult<PersonResult>().Required(nameof(request.LastName));
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


        public async Task<ServiceResult<PersonResult>> Update(int id, PersonRequest request)
        {
            var person = await _dbContext.People
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
            {
                return new ServiceResult<PersonResult>().NotFound(entityName: "Person");
            }

            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.Email = request.Email;
            person.FunctionId = request.FunctionId;

            await _dbContext.SaveChangesAsync();

            return await GetById(person.Id);
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var person = await _dbContext.People
                .FirstOrDefaultAsync(p => p.Id == id);

            //var person = new Person { Id = id, FirstName = string.Empty, LastName = string.Empty };
            //_dbContext.People.Attach(person);

            if (person is null)
            {
                return new ServiceResult().AlreadyRemoved();
            }

            _dbContext.People.Remove(person);

            await _dbContext.SaveChangesAsync();
            return new ServiceResult();
        }
    }
}
