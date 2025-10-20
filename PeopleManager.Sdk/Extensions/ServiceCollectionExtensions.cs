using Microsoft.Extensions.DependencyInjection;
using PeopleManager.Sdk.DeligatingHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PeopleManager.Sdk.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection InstallApi(this IServiceCollection services, Uri baseUrl)
        {
            services.AddScoped<AuthorizationHandlers>();

            services.AddHttpClient("PeopleManagerApi", client =>
            {
                client.BaseAddress = baseUrl;
            }).AddHttpMessageHandler<AuthorizationHandlers>();

            services.AddScoped<FunctionsSdk>();
            services.AddScoped<PeopleSdk>();
            services.AddScoped<IdentitySdk>();

            return services;
        }

        public static IServiceCollection InstallApi(this IServiceCollection services, string baseUrl)
        {
            return services.InstallApi(new Uri(baseUrl));
        }
    }
}
