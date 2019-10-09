using System.Collections.Generic;
using System.Threading.Tasks;
using curso_api.Business.Interfaces;
using curso_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace curso_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonBusiness _personBusiness;

        public PersonsController(IPersonBusiness personBusiness) {
            _personBusiness = personBusiness;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var people = await _personBusiness.FindAllAsync();
            return Ok(people);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(long id)
        {
            var person = await _personBusiness.FindByIdAsync(id);
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
            return new ObjectResult(await _personBusiness.InsertAsync(person));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] Person person)
        {
            var personUpdated = await _personBusiness.FindByIdAsync(id);
            if(personUpdated == null) {
                return NotFound();
            }
            if(id != person.Id)
                return BadRequest("Id's não correspondem");
            return new ObjectResult(await _personBusiness.UpdateAsync(person));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _personBusiness.FindByIdAsync(id);
            if(person == null)
                return NotFound();
            await _personBusiness.RemoveAsync(id);
            return NoContent();
        }
    }
}
