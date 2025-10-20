using PeopleManager.Sdk;
using PeopleManager.Sdk.Extensions;
using PeopleManager.Ui.Mvc.Settings;
using PeopleManager.Ui.Mvc.Stores;
using Vives.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var apiSettings = builder.Configuration.GetSection(nameof(ApiSettings)).Get<ApiSettings>();
if(String.IsNullOrWhiteSpace(apiSettings?.BaseUrl))
{
    throw new InvalidOperationException("ApiSettings:BaseUrl is not configured.");
}

builder.Services.InstallApi(apiSettings.BaseUrl);

builder.Services.AddAuthentication()
    .AddCookie(builder =>
    {
        builder.LoginPath = "/Identity/SignIn";
        builder.AccessDeniedPath = "/Identity/SignIn";
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITokenStore, TokenStore>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

//app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
