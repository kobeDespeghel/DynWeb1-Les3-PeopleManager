using Microsoft.EntityFrameworkCore;
using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using PeopleManager.Model;
using PeopleManager.Repository;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;

namespace PeopleManager.Services
{
    public class FunctionService
    {
        private readonly PeopleManagerDbContext _dbContext;

        public FunctionService(PeopleManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceResult<IList<FunctionResult>>> Get(string? sorting)
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

            var functions = await query
                .OrderBy(sorting)
                .ToListAsync();

            //return functions;
            return new ServiceResult<IList<FunctionResult>>()
            {
                Data = functions
            };
        }

        public async Task<ServiceResult<FunctionResult>> GetById(int id)
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

            if(function == null)
            {
                return new ServiceResult<FunctionResult>().NotFound(entityName: "Function");
            }


            //return function;
            return new ServiceResult<FunctionResult>()
            {
                Data = function
            };
        }

        public async Task<ServiceResult<FunctionResult>> Create(FunctionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new ServiceResult<FunctionResult>().Required(nameof(request.Name));
                //return new ServiceResult<FunctionResult>()
                //{
                //    Messages = new List<ServiceMessage>()
                //    {
                //        new ServiceMessage()
                //        {
                //            Code = "Required",
                //            Message = $"{nameof(request.Name)} is required",
                //            Type = ServiceMessageType.Error
                //        }
                //    }
                //};
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

        public async Task<ServiceResult<FunctionResult>> Update(int id, FunctionRequest request)
        {
            //instead of get to keep the link with the dbcontext
            var function = await _dbContext.Functions
                .FirstOrDefaultAsync(f => f.Id == id);

            if (function == null)
            {
                return new ServiceResult<FunctionResult>().NotFound(entityName: "Function");
            }

            function.Name = request.Name;
            function.Description = request.Description;
            
            await _dbContext.SaveChangesAsync();

            return await GetById(function.Id);
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var function = await _dbContext.Functions
                .FirstOrDefaultAsync(f => f.Id == id);

            //shortcut if you don't want to do a call to the db first
            //var function = new Function { Id = id, Name = string.Empty };
            //_dbContext.Functions.Attach(function);

            if (function is null)
            {
                return new ServiceResult().AlreadyRemoved();
                //return new ServiceResult
                //{
                //    Messages = new List<ServiceMessage>()
                //    {
                //        new ServiceMessage()
                //        {
                //            Code = "AlreadyRemoved",
                //            Message = "Function was already removed",
                //            Type = ServiceMessageType.Warning
                //        }
                //    }
                //};
            }

            _dbContext.Functions.Remove(function);

            await _dbContext.SaveChangesAsync();
            return new ServiceResult();
        }
    }
}
