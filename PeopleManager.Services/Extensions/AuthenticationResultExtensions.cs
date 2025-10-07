using Microsoft.AspNetCore.Identity;
using PeopleManager.Dto.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vives.Services.Model;

namespace PeopleManager.Services.Extensions
{
    public static class AuthenticationResultExtensions
    {
        public static ServiceResult<IdentityUser> LoginFailed(this ServiceResult<IdentityUser> result)
        {
            result.Messages.Add(
                new ServiceMessage()
                {
                    Code = "LoginFailed",
                    Message = "Login failed. Invalid username or password.",
                    Type = ServiceMessageType.Error
                });

            return result;
        }
    }
}
