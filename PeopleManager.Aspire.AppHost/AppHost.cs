using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<PeopleManager_Api>("API");
builder.AddProject<PeopleManager_Ui_Mvc>("MVC");
//builder.AddProject<PeopleManager_Ui_ConsoleApp>("Console");

builder.Build().Run();
