using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vives.Services.Model.Extensions
{
    public static class PagingExtensions
    {
        public static IQueryable<T> ApplyPaging<T>(this IOrderedQueryable<T> query, Paging paging)
            where T : class
        {
            if(paging.Offset < 0)
            {
                paging.Offset = 0;
            }

            if(paging.Limit <= 0)
            {
                paging.Limit = 10;
            }

            if(paging.Limit > 100)
            {
                paging.Limit = 100;
            }

            return query
                .Skip(paging.Offset)
                .Take(paging.Limit);

        }
    }
}
