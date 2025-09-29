using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Services;

namespace PeopleManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionsController(FunctionService functionService) : ControllerBase
    {
        //GET
        [HttpGet]
        public async Task<IActionResult> GetAllFunctions()
        {
            var functions = await functionService.Get();
            return Ok(functions);
        }

        //GET by Id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var function = await functionService.GetById(id);
            return Ok(function);
        }

        //CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Function function)
        {
            var createdFunction = await functionService.Create(function);
            return Ok(createdFunction);
        }

        //UPDATE
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Function function)
        {
            var updatedFunction = await functionService.Update(id, function);
            return Ok(updatedFunction);
        }

        //DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await functionService.Delete(id);
            return Ok();
        }
    }
}
