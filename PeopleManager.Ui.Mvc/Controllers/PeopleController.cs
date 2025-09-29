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
    public IActionResult Index()
    {
        var people = _personService.Find();
        return View(people);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return CreateView("Create");
    }

    [HttpPost]
    public IActionResult Create(Person person)
    {
        if (!ModelState.IsValid)
        {
            return CreateView("Create", person);
        }
        _personService.Create(person);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit([FromRoute] int id)
    {
        var person = _personService.Get(id);
        if (person is null)
        {
            return RedirectToAction("Index");
        }

        return CreateView("Edit", person);
    }

    [HttpPost]
    public IActionResult Edit([FromRoute] int id, [FromForm] Person person)
    {
        if (!ModelState.IsValid)
        {
            return CreateView("Edit", person);
        }

        _personService.Update(id, person);

        return RedirectToAction("Index");
    }



    [HttpGet]
    public IActionResult Delete([FromRoute] int id)
    {
        var person = _personService.Get(id);
        if (person is null)
        {
            return RedirectToAction("Index");
        }
        return View(person);
    }

    [HttpPost]
    [Route("[controller]/Delete/{id:int?}")]
    public IActionResult DeleteConfirmed(int id)
    {
        _personService.Delete(id);

        return RedirectToAction("Index");
    }


    private IActionResult CreateView([AspMvcView] string viewName, Person? person = null)
    {
        var functions = _functionService.Find();
        ViewBag.Functions = functions;

        if (person is null)
        {
            return View(viewName);
        }
        return View(viewName, person);
    }

}
