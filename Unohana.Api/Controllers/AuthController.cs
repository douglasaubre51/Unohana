using Microsoft.AspNetCore.Mvc;

namespace Unohana.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> SignIn()
        {
            return Created();
        }
    }
}
