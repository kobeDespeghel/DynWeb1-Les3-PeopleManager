using PeopleManager.Services;

namespace PeopleManager.Api.Installers
{
    public static class ServicesInstaller
    {
        public static WebApplicationBuilder InstallServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<FunctionService>();
            builder.Services.AddScoped<PersonService>();

            return builder;
        }
    }
}
