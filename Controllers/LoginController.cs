using System.Threading.Tasks;
using curso_api.Business.Interfaces;
using curso_api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace curso_api.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginBusiness _loginBusiness;

        public LoginController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Login([FromBody] User user)
        {
            if(user == null) return BadRequest();
            return await _loginBusiness.Login(user);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<object> Register([FromBody] User user)
        {
            if(user == null) return BadRequest();
            return await _loginBusiness.Register(user);
        }
    }
}