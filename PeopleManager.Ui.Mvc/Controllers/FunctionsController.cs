using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Dto.Requests;
//using PeopleManager.Model;
//using PeopleManager.Services;

namespace PeopleManager.Ui.Mvc.Controllers
{
    [Authorize]
    public class FunctionsController : Controller
    {
        //private readonly FunctionService _functionService;

        public FunctionsController()
        {
            //_functionService = functionService;
        }

        public async Task<IActionResult> Index()
        {
            var functions = await _functionService.Get();
            return View(functions);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FunctionRequest function)
        {
            if (!ModelState.IsValid)
            {
                return View(function);
            }

            await _functionService.Create(function);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var function = await _functionService.GetById(id);
            if (function is null)
            {
                return RedirectToAction("Index");
            }
            return View(function);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromForm]FunctionRequest function)
        {
            if (!ModelState.IsValid)
            {
                return View(function);
            }

            await _functionService.Update(id, function);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var function = await _functionService.GetById(id);
            if (function is null)
            {
                return RedirectToAction("Index");
            }
            return View(function);
        }

        [HttpPost]
        [Route("[controller]/Delete/{id:int?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _functionService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}

