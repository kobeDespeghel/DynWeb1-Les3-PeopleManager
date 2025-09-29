using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PeopleManager.Repository;
using PeopleManager.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//var connectionString = builder.Configuration.GetConnectionString(nameof(PeopleManagerDbContext));

builder.Services.AddDbContext<PeopleManagerDbContext>(options =>
{
    options.UseInMemoryDatabase(nameof(PeopleManagerDbContext));
    //options.UseSqlServer(connectionString);
});

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        //options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<PeopleManagerDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/Identity/SignIn";
    options.AccessDeniedPath = "/Identity/SignIn";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.SlidingExpiration = true;
});

builder.Services.AddScoped<FunctionService>();
builder.Services.AddScoped<PersonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<PeopleManagerDbContext>();
    if (dbContext.Database.IsInMemory())
    {
        dbContext.Seed();
    }
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
