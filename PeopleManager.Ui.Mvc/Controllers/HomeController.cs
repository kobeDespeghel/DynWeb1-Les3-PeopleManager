using Microsoft.AspNetCore.Mvc;
using PeopleManager.Sdk;

namespace PeopleManager.Ui.Mvc.Controllers;

public class HomeController(PeopleSdk peopleSdk) : Controller
{
    private readonly PeopleSdk _peopleSdk = peopleSdk;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var people = await _peopleSdk.Get();

        return View(people);
    }

    [HttpGet]
    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult About()
    {
        return View();
    }

    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //public IActionResult Error()
    //{
    //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //}
    
}
