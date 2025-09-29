using Microsoft.EntityFrameworkCore;
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

        public async Task<IList<Function>> Get()
        {
            var functions = await _dbContext.Functions.ToListAsync();
            return functions;
        }

        public async Task<Function?> GetById(int id)
        {
            var function = await _dbContext.Functions.FirstOrDefaultAsync(f => f.Id == id);
            return function;
        }

        public async Task<Function?> Create(Function function)
        {
            if (string.IsNullOrWhiteSpace(function.Name))
            {
                return null;
            }

            _dbContext.Functions.Add(function);

            await _dbContext.SaveChangesAsync();

            return function;
        }

        public async Task<Function?> Update(int id, Function function)
        {
            var dbFunction = await GetById(id);

            if (dbFunction == null)
            {
                return null;
            }

            dbFunction.Name = function.Name;
            dbFunction.Description = function.Description;
            
            await _dbContext.SaveChangesAsync();

            return dbFunction;
        }

        public async Task Delete(int id)
        {
            var function = await GetById(id);

            if (function is null)
            {
                return;
            }
            //var function = new Function { Id = id, Name = string.Empty };
            //_dbContext.Functions.Attach(function);

            _dbContext.Functions.Remove(function);

            await _dbContext.SaveChangesAsync();
        }
    }
}
