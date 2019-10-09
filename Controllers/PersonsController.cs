using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MinhaApi.Controllers.Model;
using MinhaApi.Controllers.Model.Interfaces.Service;

namespace MinhaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService) {
            _personService = personService;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var people = await _personService.FindAll();
            return Ok(people);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(long id)
        {
            var person = await _personService.FindById(id);
            if(person == null)
                return NotFound();
            return Ok(person);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            if(person == null)
                return NotFound();
            return new ObjectResult(await _personService.Insert(person));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] Person person)
        {
            var personUpdated = await _personService.FindById(id);
            if(personUpdated == null) {
                return NotFound();
            }
            if(id != person.Id)
                return BadRequest("Id's não correspondem");
            return new ObjectResult(await _personService.Update(person));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _personService.FindById(id);
            if(person == null)
                return NotFound();
            await _personService.Remove(id);
            return NoContent();
        }
    }
}
