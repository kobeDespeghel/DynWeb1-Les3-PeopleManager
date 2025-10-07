using Microsoft.EntityFrameworkCore;
using PeopleManager.Api.Installers;
using PeopleManager.Repository;
using PeopleManager.Services;

var builder = WebApplication.CreateBuilder(args);



builder.InstallRestApi()
    .InstallSwagger()
    .InstallDatabase()
    .InstallServices()
    .InstallAuthentication()
    .InstallIndentity();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    
    var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<PeopleManagerDbContext>();
    if (dbContext.Database.IsInMemory())
    {
        dbContext.Seed();
    }

}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
