using System.Threading.Tasks;
using curso_api.Business.Interfaces;
using curso_api.Data.VO;
using curso_api.Model;
using Microsoft.AspNetCore.Mvc;
using Tapioca.HATEOAS;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using curso_api.Business;

namespace curso_api.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly PersonBusiness _personBusiness;

        public PersonsController(PersonBusiness personBusiness) {
            _personBusiness = personBusiness;
        }

        // GET api/values
        [HttpGet]
        [SwaggerResponse((200), Type = typeof(List<PersonVO>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<ActionResult> Get()
        {
            var persons = await _personBusiness.FindAllAsync();
            return Ok(persons);
        }

        // GET api/values/5
        [HttpGet("GetPerson/{id}", Name = "GetPerson")]
        [SwaggerResponse((200), Type = typeof(PersonVO))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [SwaggerResponse(404)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<ActionResult> Get(long id)
        {
            var person = await _personBusiness.FindByIdAsync(id);
            if(person == null)
                return NotFound();
            return Ok(person);
        }

        // GET api/values/name
        [HttpGet("GetPersonByName", Name = "GetPersonByName")]
        [SwaggerResponse((200), Type = typeof(List<PersonVO>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [SwaggerResponse(404)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<ActionResult> Get([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var persons = await _personBusiness.FindByName(firstName, lastName);
            return Ok(persons);
        }

    // GET api/values/name
        [HttpGet("GetPersonWithPagedSearch/{sortDirection}/{pageSize}/{page}", Name = "GetPersonWithPagedSearch")]
        [SwaggerResponse((200), Type = typeof(List<PersonVO>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [SwaggerResponse(404)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<ActionResult> Get([FromQuery] string name, string sortDirection, int pageSize, int page)
        {
            var persons = await _personBusiness.FindWithPagedSearch(name, sortDirection, pageSize, page);
            return Ok(persons);
        }


        // POST api/values
        [HttpPost("CreatePerson", Name = "CreatePerson")]
        [SwaggerResponse((201), Type = typeof(PersonVO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> Post([FromBody] PersonVO person)
        {
            if(person == null)
                return NotFound();
            return new ObjectResult(await _personBusiness.InsertAsync(person));
        }

        // PUT api/values/5
        [HttpPut("UpdatePerson/{id}", Name = "UpdatePerson")]
        [SwaggerResponse((202), Type = typeof(PersonVO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> Put([FromBody] PersonVO person)
        {
            if(person == null) return BadRequest();
            var updatedPerson = await _personBusiness.UpdateAsync(person);
            if(updatedPerson == null) return BadRequest("A pessoa não consta na base de dados");
            return new ObjectResult(await _personBusiness.UpdateAsync(person));
        }

        // DELETE api/values/5
        [HttpDelete("DeletePerson/{id}", Name = "DeletePerson")]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
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
