using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Dto.Requests;
using PeopleManager.Sdk;
//using PeopleManager.Model;
//using PeopleManager.Services;

namespace PeopleManager.Ui.Mvc.Controllers
{
    //[Authorize]
    public class FunctionsController : Controller
    {
        private readonly FunctionsSdk _functionsSdk;

        public FunctionsController(FunctionsSdk functionsSdk)
        {
            _functionsSdk = functionsSdk;
        }

        public async Task<IActionResult> Index()
        {
            var functions = await _functionsSdk.Get();
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

            await _functionsSdk.Create(function);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var function = await _functionsSdk.GetById(id);
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

            await _functionsSdk.Update(id, function);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var function = await _functionsSdk.GetById(id);
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
            await _functionsSdk.Delete(id);

            return RedirectToAction("Index");
        }
    }
}

