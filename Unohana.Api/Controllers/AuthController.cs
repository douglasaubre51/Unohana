using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Unohana.Api.Services.Authentication;
using Unohana.Shared.Dtos;
using Unohana.Shared.Models;

namespace Unohana.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly CreateStudentAccount _createStudent;
        readonly SignInStudent _signInStudent;
        public AuthController(
            CreateStudentAccount createStudent,
            SignInStudent signInStudent
            )
        {
            _createStudent = createStudent;
            _signInStudent = signInStudent;
        }

        [HttpPost("/student/signin")]
        public async Task<ActionResult> StudentSignIn(SignInDto dto)
        {
            try
            {
                StudentModel? model = await _signInStudent.SignIn(dto);
                if (model is null)
                {
                    Debug.WriteLine("invalid register no!");
                    return Unauthorized();
                }
                if (model.Password != dto.Password)
                {
                    Debug.WriteLine("invalid password!");
                    return Unauthorized();
                }
                // success
                return Ok(model);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"StudentSignIn error: {ex}");
                return StatusCode(500);
            }
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
