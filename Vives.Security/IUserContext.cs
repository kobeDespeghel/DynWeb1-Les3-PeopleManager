using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vives.Security
{
    public interface IUserContext<T> where T : struct
    {
        public T? UserId { get; }
    }

    public interface IUserContext : IUserContext<int>
    {
    }
}
