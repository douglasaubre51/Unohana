using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Unohana.Api.Interfaces;
using Unohana.Api.Services.Authentication;
using Unohana.Shared.Dtos;
using Unohana.Shared.Models;

namespace Unohana.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        ITeacherRepository teacherRepository,
        CreateStudentAccount createStudent,
        SignInStudent signInStudent
            ) : ControllerBase
    {
        readonly ITeacherRepository _teacherRepository = teacherRepository;
        readonly CreateStudentAccount _createStudent = createStudent;
        readonly SignInStudent _signInStudent = signInStudent;

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
