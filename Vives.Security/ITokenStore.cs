using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vives.Security
{
    public interface ITokenStore
    {
        public string GetToken();
        public void SetToken(string token);
        public void Clear();
    }
}
