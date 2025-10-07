namespace PeopleManager.Api.Installers
{
    public static class RestApiInstaller
    {
        public static WebApplicationBuilder InstallRestApi(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddControllers();

            return builder;
        }
    }
}
