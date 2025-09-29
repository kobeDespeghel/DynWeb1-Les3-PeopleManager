using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Services;

namespace PeopleManager.Ui.Mvc.Controllers
{
    [Authorize]
    public class FunctionsController : Controller
    {
        private readonly FunctionService _functionService;

        public FunctionsController(FunctionService functionService)
        {
            _functionService = functionService;
        }

        public IActionResult Index()
        {
            var functions = _functionService.Find();
            return View(functions);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Function function)
        {
            if (!ModelState.IsValid)
            {
                return View(function);
            }

            _functionService.Create(function);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit([FromRoute] int id)
        {
            var function = _functionService.Get(id);
            if (function is null)
            {
                return RedirectToAction("Index");
            }
            return View(function);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id, [FromForm]Function function)
        {
            if (!ModelState.IsValid)
            {
                return View(function);
            }

            _functionService.Update(id, function);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete([FromRoute] int id)
        {
            var function = _functionService.Get(id);
            if (function is null)
            {
                return RedirectToAction("Index");
            }
            return View(function);
        }

        [HttpPost]
        [Route("[controller]/Delete/{id:int?}")]
        public IActionResult DeleteConfirmed(int id)
        {
            _functionService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}

