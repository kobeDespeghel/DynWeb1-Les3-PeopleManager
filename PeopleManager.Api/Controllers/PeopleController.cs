using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Dto.Filters;
using PeopleManager.Dto.Requests;
using PeopleManager.Model;
using PeopleManager.Services;
using Vives.Services.Model;

namespace PeopleManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController(PersonService personService) : ControllerBase
    {

        //GET
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPeople([FromQuery]Paging paging, [FromQuery]string? sorting, [FromQuery]PersonFilter? filter)
        {
            var result = await personService.Get(paging, sorting, filter);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        //GET by Id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var result = await personService.GetById(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        //CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonRequest person)
        {
            var result = await personService.Create(person);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        //UPDATE
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PersonRequest person)
        {

            var result = await personService.Update(id, person);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        //DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await personService.Delete(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }   


    }
}
