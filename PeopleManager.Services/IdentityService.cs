using Microsoft.AspNetCore.Identity;
using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using PeopleManager.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vives.Services.Model;

namespace PeopleManager.Services
{
    public class IdentityService(UserManager<IdentityUser> userManager)
    {
        //Sign in
        public async Task<ServiceResult<IdentityUser>> SignIn(SignInRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return new ServiceResult<IdentityUser>().LoginFailed();

            var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
                return new ServiceResult<IdentityUser>().LoginFailed();

            return new ServiceResult<IdentityUser>();
        }


        //register
        public async Task<ServiceResult<IdentityUser>> Register(RegisterRequest request)
        {
            var user = new IdentityUser(request.Email);
            user.Email = request.Email;
            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var serviceResult = new ServiceResult<IdentityUser>();
                foreach (var error in result.Errors)
                {
                    serviceResult.Messages.Add(new ServiceMessage
                    {
                        Code = error.Code,
                        Message = error.Description,
                        Type = ServiceMessageType.Error
                    });
                }
                return serviceResult;
            }

            return new ServiceResult<IdentityUser>(user);
        }
    }
}
