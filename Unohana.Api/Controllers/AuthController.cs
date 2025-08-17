using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Unohana.Api.Interfaces;
using Unohana.Shared.Dtos;
using Unohana.Shared.Models;

namespace Unohana.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        ITeacherRepository teacherRepository,
        IStudentRepository studentRepository
            ) : ControllerBase
    {
        readonly ITeacherRepository _teacherRepository = teacherRepository;
        readonly IStudentRepository _studentRepository = studentRepository;

        [HttpPost("teacher/signin")]
        public async Task<ActionResult> TeacherSignIn(SignInDto dto)
        {
            try
            {
                TeacherModel? model = await _teacherRepository
                    .GetByEmployeeId(dto.IdentificationNumber);

                if (model is null)
                {
                    Debug.WriteLine("invalid employee id!");
                    return Unauthorized();
                }
                if (model.Password != dto.Password)
                {
                    Debug.WriteLine("invalid password!");
                    return Unauthorized();
                }
                // success
                // create a cookie!
                List<Claim> claims = new()
                {
                    new Claim(ClaimTypes.NameIdentifier,model.Id!.ToString()),
                    new Claim(ClaimTypes.Name,model.Username),
                    new Claim(ClaimTypes.Email,model.Email),
                    new Claim(ClaimTypes.Role,"Teacher")
                };

                ClaimsIdentity identity = new(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                    );

                AuthenticationProperties properties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    properties
                    );

                Debug.WriteLine($"teacher logged in at:{DateTime.UtcNow}");

                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"TeacherSignIn error: {ex}");
                return StatusCode(500);
            }
        }

        [HttpPost("teacher/signup")]
        public async Task<ActionResult> TeacherSignup(SignUpDto dto)
        {
            try
            {
                TeacherModel model = new()
                {
                    EmployeeId = dto.IdentificationNumber,
                    Username = dto.Username,
                    Email = dto.Email,
                    Password = dto.Password
                };
                await _teacherRepository.Add(model);
                return Created();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("TeacherSignUp error: " + ex);
                return StatusCode(500);
            }
        }

        [HttpPost("student/signin")]
        public async Task<ActionResult> StudentSignIn(SignInDto dto)
        {
            try
            {
                StudentModel? model = await _studentRepository
                    .GetByRegisterNumber(dto.IdentificationNumber);

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
                StudentModel model = new()
                {
                    RegisterNumber = dto.IdentificationNumber,
                    Username = dto.Username,
                    Email = dto.Email,
                    Password = dto.Password,
                };
                await _studentRepository.Add(model);
                // success
                return Created();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"StudentSignUp error: {e}");
                return BadRequest();
            }
        }
    }
}
