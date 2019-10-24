using System.Threading.Tasks;
using curso_api.Business.Interfaces;
using curso_api.Data.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace curso_api.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    [Authorize("Bearer")]
    public class BooksController : ControllerBase
    {
        private readonly IBusiness<BookVO> _bookBusiness;

        public BooksController(IBusiness<BookVO> bookBusiness) {
            _bookBusiness = bookBusiness;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var books = await _bookBusiness.FindAllAsync();
            return Ok(books);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(long id)
        {
            var book = await _bookBusiness.FindByIdAsync(id);
            if(book == null)
                return NotFound();
            return Ok(book);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookVO book)
        {
            if(book == null)
                return NotFound();
            return new ObjectResult(await _bookBusiness.InsertAsync(book));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] BookVO book)
        {
            if(book == null) return BadRequest();
            var updatedPerson = await _bookBusiness.UpdateAsync(book);
            if(updatedPerson == null) return BadRequest("A pessoa não consta na base de dados");
            return new ObjectResult(await _bookBusiness.UpdateAsync(book));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookBusiness.FindByIdAsync(id);
            if(book == null)
                return NotFound();
            await _bookBusiness.RemoveAsync(id);
            return NoContent();
        }
    }
}