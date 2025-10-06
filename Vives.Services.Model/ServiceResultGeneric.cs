using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vives.Services.Model
{
    public class ServiceResult<T>: ServiceResult
    {
        public ServiceResult(T? data = default)
        {
            data = data;
        }
        public T? Data { get; set; }
    }
}
