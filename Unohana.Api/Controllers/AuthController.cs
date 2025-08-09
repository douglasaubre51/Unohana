using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Unohana.Api.Services.Authentication;
using Unohana.Shared.Dtos;

namespace Unohana.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly StudentAuthentication _authentication;
        public AuthController(StudentAuthentication authentication)
        {
            _authentication = authentication;
        }

        [HttpPost("/student-signup")]
        public async Task<ActionResult> StudentSignUp(SignUpDto dto)
        {
            try
            {
                await _authentication.SignUp(dto);
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
