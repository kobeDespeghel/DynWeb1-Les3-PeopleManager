using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vives.Services.Model
{
    public class ServiceMessage
    {
        public required string Code { get; set; }
        public required string Message { get; set; }

        public ServiceMessageType Type { get; set; }
        public string? PropertyName { get; set; }

    }
}
