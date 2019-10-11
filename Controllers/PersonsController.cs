using System.Threading.Tasks;
using curso_api.Business.Interfaces;
using curso_api.Data.VO;
using curso_api.Model;
using Microsoft.AspNetCore.Mvc;
using Tapioca.HATEOAS;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace curso_api.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IBusiness<PersonVO> _personBusiness;

        public PersonsController(IBusiness<PersonVO> personBusiness) {
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
            var people = await _personBusiness.FindAllAsync();
            return Ok(people);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetPerson")]
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

        // POST api/values
        [HttpPost(Name = "CreatePerson")]
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
        [HttpPut("{id}", Name = "UpdatePerson")]
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
        [HttpDelete("{id}", Name = "DeletePerson")]
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
