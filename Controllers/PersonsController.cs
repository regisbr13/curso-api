using System.Threading.Tasks;
using curso_api.Business.Interfaces;
using curso_api.Data.VO;
using curso_api.Model;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Post([FromBody] PersonVO person)
        {
            if(person == null)
                return NotFound();
            return new ObjectResult(await _personBusiness.InsertAsync(person));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] PersonVO person)
        {
            if(person == null) return BadRequest();
            var updatedPerson = await _personBusiness.UpdateAsync(person);
            if(updatedPerson == null) return BadRequest("A pessoa não consta na base de dados");
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
