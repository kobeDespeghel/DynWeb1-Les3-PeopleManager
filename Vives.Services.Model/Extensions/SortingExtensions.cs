using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vives.Services.Model.Extensions
{
    public static class SortingExtensions
    {
        public static string ToOrderQueryString(this Sorting sorting)
        {
            if (String.IsNullOrWhiteSpace(sorting.PropertyName))
            {
                return String.Empty;
            }

            var order = sorting.IsDescending ? "desc" : "";

            return $"{ sorting.PropertyName} {order}".Trim();
        }
    }
}
