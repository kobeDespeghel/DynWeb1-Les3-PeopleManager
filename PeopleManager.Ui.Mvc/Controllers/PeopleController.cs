using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Services;

namespace PeopleManager.Ui.Mvc.Controllers;

[Authorize]
public class PeopleController : Controller
{
    private readonly PersonService _personService;
    private readonly FunctionService _functionService;

    public PeopleController(PersonService personService, FunctionService functionService)
    {
        _personService = personService;
        _functionService = functionService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var people = await _personService.Get();
        return View(people);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return CreateView("Create");
    }

    [HttpPost]
    public async Task<IActionResult> Create(Person person)
    {
        if (!ModelState.IsValid)
        {
            return CreateView("Create", person);
        }
        await _personService.Create(person);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit([FromRoute] int id)
    {
        var person = await _personService.GetById(id);
        if (person is null)
        {
            return RedirectToAction("Index");
        }

        return CreateView("Edit", person);
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] Person person)
    {
        if (!ModelState.IsValid)
        {
            return CreateView("Edit", person);
        }

        await _personService.Update(id, person);

        return RedirectToAction("Index");
    }



    [HttpGet]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var person = await _personService.GetById(id);
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
        await _personService.Delete(id);

        return RedirectToAction("Index");
    }


    private async Task<IActionResult> CreateView([AspMvcView] string viewName, Person? person = null)
    {
        var functions = await _functionService.Get();
        ViewBag.Functions = functions;

        if (person is null)
        {
            return View(viewName);
        }
        return View(viewName, person);
    }

}
