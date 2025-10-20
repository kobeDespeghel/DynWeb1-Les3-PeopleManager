using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PeopleManager.Dto.Requests;
using PeopleManager.Sdk;
using PeopleManager.Sdk.Extensions;
using PeopleManager.Ui.ConsoleApp.Settings;
using PeopleManager.Ui.ConsoleApp.Stores;
using Vives.Security;



var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var configuration = builder.Build();


var apiSettings = configuration.GetSection(nameof(ApiSettings)).Get<ApiSettings>();
if (string.IsNullOrWhiteSpace(apiSettings?.BaseUrl))
{
    Console.WriteLine("ApiSettings:BaseUrl is not configured in appsettings.json");
    throw new InvalidOperationException("ApiSettings:BaseUrl is not configured in appsettings.json");
}

//build Service Collection

var serviceCollection = new ServiceCollection();
serviceCollection.InstallApi(apiSettings.BaseUrl);
serviceCollection.AddScoped<ITokenStore, TokenStore>();

var serviceProvider = serviceCollection.BuildServiceProvider();

//get sdks
var identitySdk = serviceProvider.GetRequiredService<IdentitySdk>();
var peopleSdk = serviceProvider.GetRequiredService<PeopleSdk>();
var functionsSdk = serviceProvider.GetRequiredService<FunctionsSdk>();

//sign in 
var signInRequest = new SignInRequest
{
    Email = "kobe.desp@gmail.be",
    Password = "Test123$" //check Repository
};

var authenticationResult = await identitySdk.SignIn(signInRequest);
if (!authenticationResult.IsSuccess)
{
    Console.WriteLine("Sign in failed");
    Console.WriteLine("Press Enter to exit...");
    Console.ReadLine();
    return;
}

var tokenStore = serviceProvider.GetRequiredService<ITokenStore>();
tokenStore.SetToken(authenticationResult.Token ?? string.Empty);

//list people
var people = await peopleSdk.Get();
foreach (var person in people.Data)
{
    Console.WriteLine($"{person.Id} - {person.FirstName} {person.LastName}");
}

//list functions
var functions = await functionsSdk.Get();
foreach (var function in functions.Data)
{
    Console.WriteLine($"{function.Id} - {function.Name}");
}

Console.ReadLine();