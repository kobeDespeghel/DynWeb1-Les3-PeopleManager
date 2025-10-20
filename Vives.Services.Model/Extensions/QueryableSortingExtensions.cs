using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Vives.Services.Model.Extensions
{
    public static class QueryableSortingExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, Sorting sorting)
            where T : class
        {
            var sortString = sorting.ToOrderQueryString();
            return source.OrderBy(sortString);

        }


        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, Sorting sorting)
            where T : class
        {
            var sortString = sorting.ToOrderQueryString();
            return source.ThenBy(sortString);
        }


        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string? sortString)
            where T : class
        {
            if(String.IsNullOrWhiteSpace(sortString))
            {
                return (IOrderedQueryable<T>)source;
            }

            // Split the sorting string by commas
            var sortingPairs = SortingPairs(sortString);

            IOrderedQueryable<T>? orderedQuery = null;

            foreach(var sortingPair in sortingPairs)
            {
                //get property info
                var propertyInfo = typeof(T).GetProperty(sortingPair.PropertyName);
                if(propertyInfo == null)
                {
                    //property not found, skip
                    continue;
                }

                //create lambda expression
                var parameter = Expression.Parameter(typeof(T), "x");
                var propertyExpression = Expression.Property(parameter, propertyInfo);
                var lambda = Expression.Lambda(propertyExpression, parameter);


                //apply sorting based on asc or desc
                if(orderedQuery == null)
                {
                    orderedQuery = sortingPair.IsDescending
                        ? Queryable.OrderByDescending(source, (dynamic)lambda)
                        : Queryable.OrderBy(source, (dynamic)lambda);
                }
                else
                {
                    orderedQuery = sortingPair.IsDescending
                        ? Queryable.ThenByDescending(orderedQuery, (dynamic)lambda)
                        : Queryable.ThenBy(orderedQuery, (dynamic)lambda);
                }
            }

            return orderedQuery ?? (IOrderedQueryable<T>)source;
        }

        private static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string? sortString)
            where T : class
        {
            if (String.IsNullOrWhiteSpace(sortString))
            {
                return source;
            }


            // Split the sorting string by commas
            var sortingPairs = SortingPairs(sortString);

            IOrderedQueryable<T>? orderedQuery = source;


            foreach (var sortingPair in sortingPairs)
            {
                //get property info
                var propertyInfo = typeof(T).GetProperty(sortingPair.PropertyName);
                if (propertyInfo == null)
                {
                    //property not found, skip
                    continue;
                }

                //create lambda expression
                var parameter = Expression.Parameter(typeof(T), "x");
                var propertyExpression = Expression.Property(parameter, propertyInfo);
                var lambda = Expression.Lambda(propertyExpression, parameter);

                orderedQuery = sortingPair.IsDescending
                    ? Queryable.ThenByDescending(orderedQuery, (dynamic)lambda)
                    : Queryable.ThenBy(orderedQuery, (dynamic)lambda);
            }

            return orderedQuery ?? source;
        }


        private static IEnumerable<Sorting> SortingPairs(string sortString)
        {
            var sortingPairs = sortString.Split(',')
                .Select(part => part.Trim())
                .Select(part => new Sorting
                {
                    PropertyName = part.EndsWith(" desc", StringComparison.OrdinalIgnoreCase)
                    ? part.Substring(0, part.Length - 5).Trim() //minus " desc"
                    : part.EndsWith(" asc", StringComparison.OrdinalIgnoreCase)
                        ? part.Substring(0, part.Length - 4).Trim() //minus " asc"
                        : part.Trim(),

                    IsDescending = part.EndsWith(" desc", StringComparison.OrdinalIgnoreCase)
                });

            return sortingPairs;
        }
    }
}
