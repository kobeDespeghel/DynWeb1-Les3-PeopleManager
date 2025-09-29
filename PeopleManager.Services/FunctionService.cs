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

        public IList<Function> Find()
        {
            var functions = _dbContext.Functions.ToList();
            return functions;
        }

        public Function? Get(int id)
        {
            var function = _dbContext.Functions.FirstOrDefault(f => f.Id == id);
            return function;
        }

        public Function? Create(Function function)
        {
            if (string.IsNullOrWhiteSpace(function.Name))
            {
                return null;
            }

            _dbContext.Functions.Add(function);

            _dbContext.SaveChanges();

            return function;
        }

        public Function? Update(int id, Function function)
        {
            var dbFunction = Get(id);

            if (dbFunction == null)
            {
                return null;
            }

            dbFunction.Name = function.Name;
            dbFunction.Description = function.Description;
            
            _dbContext.SaveChanges();

            return dbFunction;
        }

        public void Delete(int id)
        {
            var function = Get(id);

            if (function is null)
            {
                return;
            }
            //var function = new Function { Id = id, Name = string.Empty };
            //_dbContext.Functions.Attach(function);

            _dbContext.Functions.Remove(function);

            _dbContext.SaveChanges();
        }
    }
}
