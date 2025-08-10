using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Unohana.Api.Interfaces;
using Unohana.Shared.Models.SeedModels;

namespace Unohana.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        readonly IStudentInfoRepository _studentRepository;
        public VerificationController(IStudentInfoRepository studentRepository)
        {
            _studentRepository = studentRepository;
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
