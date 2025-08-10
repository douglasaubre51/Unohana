using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Unohana.Api.Services.Email;
using Unohana.Api.Services.Otp;
using Unohana.Shared.Dtos;

namespace Unohana.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        readonly CreateOtp _studentOtp;
        readonly SendOtpInEmail _sendOtpInEmail;
        public OtpController(CreateOtp studentOtp, SendOtpInEmail sendOtpInEmail)
        {
            _studentOtp = studentOtp;
            _sendOtpInEmail = sendOtpInEmail;
        }
        [HttpPost("student/send-email")]
        public ActionResult SendEmailToClient(OtpInEmailDto dto)
        {
            try
            {
                string otp = _studentOtp.Create();
                _sendOtpInEmail.Send(otp, dto.Username, dto.Email);
                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SendOtpInEmail error: {ex}");
                return StatusCode(500);
            }
        }
    }
}
