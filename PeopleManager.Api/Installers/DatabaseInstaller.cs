using Microsoft.EntityFrameworkCore;
using PeopleManager.Repository;

namespace PeopleManager.Api.Installers
{
    public static class DatabaseInstaller
    {
        public static WebApplicationBuilder InstallDatabase(this WebApplicationBuilder builder)
        {
            //var connectionString = builder.Configuration.GetConnectionString(nameof(PeopleManagerDbContext));

            builder.Services.AddDbContext<PeopleManagerDbContext>(options =>
            {
                options.UseInMemoryDatabase(nameof(PeopleManagerDbContext));
                //options.UseSqlServer(connectionString);
            });

            return builder;
        }

    }
}
