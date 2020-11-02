using System.Threading.Tasks;
using ApiResource.Models;
using ApiResource.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiResource.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticationRequest request)
        {
            var response = await _userService.Authenticate(request.Username, request.Password);

            if (response == null)
                return BadRequest(new {message = "Username or password is incorrect."});

            return Ok(response);
        }
    }
}