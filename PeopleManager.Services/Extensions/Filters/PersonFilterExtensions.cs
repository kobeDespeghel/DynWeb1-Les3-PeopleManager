using PeopleManager.Dto.Filters;
using PeopleManager.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManager.Services.Extensions.Filters
{
    public static class PersonFilterExtensions
    {
        public static IQueryable<Person> ApplyFilter(this IQueryable<Person> query, PersonFilter? filter)
        {
            if (filter is null)
                return query;

            if (!String.IsNullOrWhiteSpace(filter.Search))
            {
                var searchCriteria = filter.Search.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach(var searchText in searchCriteria)
                {
                    query = query.Where(person =>
                        person.FirstName.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)
                        || person.LastName.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)
                        || (person.Email != null
                            && person.Email.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
                        || person.Function != null
                            && person.Function.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)
                    );
                }
            }

            if (!String.IsNullOrWhiteSpace(filter.FirstName))
                query = query.Where(person => person.FirstName.Contains(filter.FirstName, StringComparison.CurrentCultureIgnoreCase));

            if (!String.IsNullOrWhiteSpace(filter.LastName))
                query = query.Where(person => person.LastName.Contains(filter.LastName, StringComparison.CurrentCultureIgnoreCase));

            if (filter.UseEmailFilter)
            {
                query = query.Where(person => (filter.Email == null && person.Email == null)
                    || (person.Email != null
                        && filter.Email != null
                        && person.Email.Contains(filter.Email, StringComparison.CurrentCultureIgnoreCase)
                    )
                );
            }

            if(!String.IsNullOrWhiteSpace(filter.FunctionName))
            {
                query = query.Where(person => person.Function != null
                    && person.Function.Name.Contains(filter.FunctionName, StringComparison.CurrentCultureIgnoreCase)
                );
            }

            if (filter.UseFunctionFilter)
                query = query.Where(person => person.FunctionId == filter.FunctionId);

            return query;
        }
    }
}
