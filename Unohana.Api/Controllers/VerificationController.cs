using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Unohana.Api.Interfaces;
using Unohana.Shared.Models.SeedModels;

namespace Unohana.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController(
        IStudentInfoRepository studentRepository,
        ITeacherInfoRepository teacherRepository
        ) : ControllerBase
    {
        readonly IStudentInfoRepository _studentRepository = studentRepository;
        readonly ITeacherInfoRepository _teacherRepository = teacherRepository;

        [HttpGet("teacher/{EmployeeId}")]
        public async Task<ActionResult> TeacherVerification(double EmployeeId)
        {
            try
            {
                TeacherCSVModel? teacher = await _teacherRepository.GetByEmployeeId(EmployeeId);
                if (teacher is null)
                    return NotFound();
                // success
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"TeacherVerification error: {ex}");
                return StatusCode(500);
            }
        }

        [HttpGet("student/{RegisterNumber}")]
        public async Task<ActionResult> StudentVerification(double RegisterNumber)
        {
            try
            {
                StudentCSVModel result = await _studentRepository.GetByRegisterNumber(RegisterNumber);
                if (result is null)
                {
                    return BadRequest();
                }
                // success
                return Ok(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"StudentVerification error: {ex}");
                return StatusCode(500);
            }
        }
    }
}
