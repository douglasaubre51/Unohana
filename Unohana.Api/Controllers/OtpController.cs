using Microsoft.AspNetCore.Mvc;
using OtpNet;
using System.Diagnostics;
using Unohana.Api.Services.Email;
using Unohana.Api.Services.Otp;
using Unohana.Shared.Dtos;

namespace Unohana.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController(
        CreateOtp studentOtp,
        SendOtpInEmail sendOtpInEmail,
        SaveOtp saveOtp,
        VerifyOtp verifyOtp
        ) : ControllerBase
    {
        readonly CreateOtp _studentOtp = studentOtp;
        readonly SaveOtp _saveOtp = saveOtp;
        readonly SendOtpInEmail _sendOtpInEmail = sendOtpInEmail;
        readonly VerifyOtp _verifyOtp = verifyOtp;

        [HttpPost("student/verify-otp")]
        public ActionResult VerifyOtpForClient(OtpVerifyDto dto)
        {
            try
            {
                bool isValid = _verifyOtp.Verify(dto);
                if (!isValid)
                    return Unauthorized();
                // success
                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"VerifyOtpForClient error: {ex}");
                return StatusCode(500);
            }
        }
        [HttpPost("student/send-email")]
        public ActionResult SendEmailToClient(OtpInEmailDto dto)
        {
            try
            {
                Debug.WriteLine($"SendEmailToClient : {dto.RegisterNumber}");
                Totp Totp = _studentOtp.Create(out byte[] secretKey, out string otp);
                _saveOtp.Save(dto.RegisterNumber, secretKey);
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
