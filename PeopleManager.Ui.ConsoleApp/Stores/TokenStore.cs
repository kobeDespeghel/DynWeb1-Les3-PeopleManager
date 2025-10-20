using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vives.Security;

namespace PeopleManager.Ui.ConsoleApp.Stores
{
    public class TokenStore : ITokenStore
    {
        private static string? Token { get; set; }

        public void Clear()
        {
            Token = null;
        }

        public string GetToken()
        {
            return Token ?? string.Empty;

        }

        public void SetToken(string token)
        {
            Token = token;
        }
    }
}
