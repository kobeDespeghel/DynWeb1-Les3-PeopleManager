using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Dto.Requests;
using PeopleManager.Sdk;
using PeopleManager.Ui.Mvc.Extensions;

namespace PeopleManager.Ui.Mvc.Controllers
{
    [Authorize]
    public class FunctionsController : Controller
    {
        private readonly FunctionsSdk _functionsSdk;

        public FunctionsController(FunctionsSdk functionsSdk)
        {
            _functionsSdk = functionsSdk;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _functionsSdk.Get();
            if (!result.IsSuccess)
            {
                ModelState.AddServiceMessages(result.Messages);
                return View();
            }
            return View(result.Data);
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

            var result = await _functionsSdk.Create(function);

            if (!result.IsSuccess)
            {
                ModelState.AddServiceMessages(result.Messages);
                return View(function);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var result = await _functionsSdk.GetById(id);

            if (!result.IsSuccess)
            {
                ModelState.AddServiceMessages(result.Messages);
                return RedirectToAction("Index");
            }

            var function = result.Data;

            if (function is null)
            {
                return RedirectToAction("Index");
            }

            var request = new FunctionRequest
            {
                Name = function.Name,
                Description = function.Description,
            };

            return View(function);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromForm]FunctionRequest function)
        {
            if (!ModelState.IsValid)
            {
                return View(function);
            }

            var result = await _functionsSdk.Update(id, function);

            if (!result.IsSuccess)
            {
                ModelState.AddServiceMessages(result.Messages);
                return View(function);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _functionsSdk.GetById(id);

            if (!result.IsSuccess)
            {
                ModelState.AddServiceMessages(result.Messages);
                return RedirectToAction("Index");
            }

            var function = result.Data;

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
            var result = await _functionsSdk.Delete(id);
            if (!result.IsSuccess)
            {
                ModelState.AddServiceMessages(result.Messages);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}

