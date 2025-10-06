using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Dto.Requests;
using PeopleManager.Sdk;

namespace PeopleManager.Ui.Mvc.Controllers;

//[Authorize]
public class PeopleController : Controller
{
    private readonly PeopleSdk _peopleSdk;
    private readonly FunctionsSdk _functionsSdk;

    public PeopleController(PeopleSdk peopleSdk, FunctionsSdk functionsSdk)
    {
        _peopleSdk = peopleSdk;
        _functionsSdk = functionsSdk;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var people = await _peopleSdk.Get();
        return View(people);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return await CreateView("Create");
    }

    [HttpPost]
    public async Task<IActionResult> Create(PersonRequest request)
    {
        if (!ModelState.IsValid)
        {
            return await CreateView("Create", request);
        }
        await _peopleSdk.Create(request);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit([FromRoute] int id)
    {
        var person = await _peopleSdk.GetById(id);
        if (person is null)
        {
            return RedirectToAction("Index");
        }

        var request = new PersonRequest
        {
            FirstName = person.FirstName,
            LastName = person.LastName,
            Email = person.Email,
            FunctionId = person.FunctionId,
        };

        return await CreateView("Edit", request);
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] PersonRequest request)
    {
        if (!ModelState.IsValid)
        {
            return await CreateView("Edit", request);
        }

        await _peopleSdk.Update(id, request);

        return RedirectToAction("Index");
    }



    [HttpGet]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var person = await _peopleSdk.GetById(id);
        if (person is null)
        {
            return RedirectToAction("Index");
        }
        return View(person);
    }

    [HttpPost]
    [Route("[controller]/Delete/{id:int?}")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _peopleSdk.Delete(id);

        return RedirectToAction("Index");
    }


    private async Task<IActionResult> CreateView([AspMvcView] string viewName, PersonRequest? request = null)
    {
        var functions = await _functionsSdk.Get();
        ViewBag.Functions = functions;

        if (request is null)
        {
            return View(viewName);
        }
        return View(viewName, request);
    }

}
