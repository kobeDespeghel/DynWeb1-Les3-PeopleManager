using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Services;

namespace PeopleManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController(PersonService personService) : ControllerBase
    {

        //GET
        [HttpGet]
        public async Task<IActionResult> GetAllPeople()
        {
            var people = await personService.Get();
            return Ok(people);
        }

        //GET by Id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var person = await personService.GetById(id);
            return Ok(person);
        }


        //CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Person person)
        {
            var createdPerson = await personService.Create(person);
            return Ok(createdPerson);
        }

        //UPDATE
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Person person)
        {

            var updatedPerson = await personService.Update(id, person);
            return Ok(updatedPerson);
        }

        //DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await personService.Delete(id);
            return Ok();
        }   


    }
}
