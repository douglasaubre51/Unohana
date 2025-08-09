using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Unohana.Shared.Dtos;

namespace Unohana.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("/student-signup")]
        public async Task<ActionResult> StudentSignUp(SignUpDto dto)
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.WriteLine($"student signup error: {e}");
                return BadRequest();
            }

            return Created();
        }
    }
}
