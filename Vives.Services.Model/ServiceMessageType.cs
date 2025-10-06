using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vives.Services.Model
{
    public enum ServiceMessageType
    {
        Critical = 3, // is on top because it's default
        Info = 0,
        Warning = 1,
        Error = 2
    }
}
