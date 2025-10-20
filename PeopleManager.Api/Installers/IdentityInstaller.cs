using Microsoft.AspNetCore.Identity;
using PeopleManager.Repository;

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

            return builder;
        }
    }
}
