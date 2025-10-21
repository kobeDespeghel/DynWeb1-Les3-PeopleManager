using Microsoft.AspNetCore.Mvc;
using PeopleManager.Sdk;
using PeopleManager.Ui.Mvc.Extensions;
using Vives.Services.Model;

namespace PeopleManager.Ui.Mvc.Controllers;

public class HomeController(PeopleSdk peopleSdk) : Controller
{
    private readonly PeopleSdk _peopleSdk = peopleSdk;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _peopleSdk.Get(new Paging());
        if (!result.IsSuccess)
        {
            ModelState.AddServiceMessages(result.Messages);
            return View();
        }

        return View(result.Data);
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
