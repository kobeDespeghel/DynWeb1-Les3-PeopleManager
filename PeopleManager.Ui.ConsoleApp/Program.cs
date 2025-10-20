using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PeopleManager.Dto.Requests;
using PeopleManager.Sdk;
using PeopleManager.Sdk.Extensions;
using PeopleManager.Ui.ConsoleApp.Settings;
using PeopleManager.Ui.ConsoleApp.Stores;
using Vives.Security;
using Vives.Services.Model;



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
Console.WriteLine();
Console.WriteLine("=================================");
Console.WriteLine("PEOPLE");
Console.WriteLine("=================================");
Console.WriteLine();



var peopleResult = await peopleSdk.Get(new Paging { Offset = 2, Limit = 5 }, "FunctionName, LastName, FirstName");
if (peopleResult.IsSuccess && peopleResult.Data is not null)
{
    foreach (var person in peopleResult.Data)
    {
        Console.WriteLine($"{person.Id} - {person.FirstName} {person.LastName} ({person.FunctionName})");
    }

    var countMessage =
        $"Retrieved {peopleResult.Data.Count()} records from {peopleResult.Paging.Limit} requested and a total of {peopleResult.TotalCount}, skipped {peopleResult.Paging.Offset}.";

    Console.WriteLine(countMessage);
    var sortMessage = $"Sorted by: {peopleResult.Sorting}";
    Console.WriteLine(sortMessage);
}

//list functions
Console.WriteLine();
Console.WriteLine("=================================");
Console.WriteLine("FUNCTIONS");
Console.WriteLine("=================================");
Console.WriteLine();

var functionsResult = await functionsSdk.Get(new Paging(), "NumberOfPeople desc");
if (functionsResult.IsSuccess || functionsResult.Data is not null)
{
    foreach (var function in functionsResult.Data)
    {
        Console.WriteLine($"{function.Id} - {function.Name} ({function.NumberOfPeople})");
    }
}

Console.WriteLine("=================================");
Console.ReadLine();