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
        readonly CreateStudentAccount _createStudent;
        public AuthController(CreateStudentAccount createStudent)
        {
            _createStudent = createStudent;
        }

        [HttpPost("student/signup")]
        public async Task<ActionResult> StudentSignUp(SignUpDto dto)
        {
            try
            {
                await _createStudent.Create(dto);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"StudentSignUp error: {e}");
                return BadRequest();
            }
            return Created();
        }
    }
}
