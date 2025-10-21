using Microsoft.AspNetCore.Identity;
using PeopleManager.Api.Security;
using PeopleManager.Repository;
using Vives.Security;

namespace PeopleManager.Api.Installers
{
    public static class IdentityInstaller
    {
        public static WebApplicationBuilder InstallIndentity(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentityCore<IdentityUser>(options =>
            {
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<PeopleManagerDbContext>();

            builder.Services.AddHttpContextAccessor(); 
            builder.Services.AddScoped<IUserContext<Guid>, HttpContextUserContext>();

            return builder;
        }
    }
}
