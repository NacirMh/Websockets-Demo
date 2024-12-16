using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebSocketAPI.Interfaces;

namespace WebSocketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenManager _tokenManager;
        public AuthenticationController(ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        [HttpPost]
        public IActionResult Authenticate(string user, string password)
        {
            if (_tokenManager.Authenticate(user, password))
            {
                return Ok(_tokenManager.NewToken());
            }
            else
            {
                return BadRequest("wrong info");
            }
        }
    }
}
