using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebSocketAPI.Filters;

namespace WebSocketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {

        [HttpGet("Details")]
        [TokenAuthenticationFilter]
        public IActionResult GetDetails()
        {
            return Ok("Admin");
        }
    }
}
