using Microsoft.EntityFrameworkCore;
using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using PeopleManager.Model;
using PeopleManager.Repository;

namespace PeopleManager.Services
{
    public class FunctionService
    {
        private readonly PeopleManagerDbContext _dbContext;

        public FunctionService(PeopleManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<FunctionResult>> Get()
        {
            //var functions = await _dbContext.Functions
            //    .Select(f => new FunctionResult
            //    {
            //        Id = f.Id,
            //        Name = f.Name,
            //        Description = f.Description,
            //        NumberOfPeople = f.People.Count
            //    })
            //    .ToListAsync();
            var query = _dbContext.Functions
                .Select(f => new FunctionResult
                {
                    Id = f.Id,
                    Name = f.Name,
                    Description = f.Description,
                    NumberOfPeople = f.People.Count
                });
            var functions = await query.ToListAsync();
            return functions;
        }

        public async Task<FunctionResult?> GetById(int id)
        {
            var function = await _dbContext.Functions
                //same as above = reeaal baadman
                .Select(f => new FunctionResult
                {
                    Id = f.Id,
                    Name = f.Name,
                    Description = f.Description,
                    NumberOfPeople = f.People.Count
                })
                .FirstOrDefaultAsync(f => f.Id == id);
            return function;
        }

        public async Task<FunctionResult?> Create(FunctionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return null;
            }

            var newFunction = new Function
            {
                Name = request.Name,
                Description = request.Description
            };

            _dbContext.Functions
                .Add(newFunction);

            await _dbContext.SaveChangesAsync();

            return await GetById(newFunction.Id);
        }

        public async Task<FunctionResult?> Update(int id, FunctionRequest request)
        {
            //instead of get to keep the link with the dbcontext
            var function = await _dbContext.Functions
                .FirstOrDefaultAsync(f => f.Id == id);

            if (function == null)
            {
                return null;
            }

            function.Name = request.Name;
            function.Description = request.Description;
            
            await _dbContext.SaveChangesAsync();

            return await GetById(function.Id);
        }

        public async Task Delete(int id)
        {
            var function = await _dbContext.Functions
                .FirstOrDefaultAsync(f => f.Id == id);

            //shortcut if you don't want to do a call to the db first
            //var function = new Function { Id = id, Name = string.Empty };
            //_dbContext.Functions.Attach(function);

            if (function is null)
            {
                return;
            }

            _dbContext.Functions.Remove(function);

            await _dbContext.SaveChangesAsync();
        }
    }
}
