using Microsoft.OpenApi.Models;

namespace PeopleManager.Api.Installers
{
    public static class SwaggerInstaller
    {

        public static WebApplicationBuilder InstallSwagger(this WebApplicationBuilder builder)
        {
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization Bearer {Token}\"",
                    Name = "Authorization",
                    Scheme = "Bearer"
                });
            });

            return builder;
        }
    }
}
