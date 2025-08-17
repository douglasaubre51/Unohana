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
        CreateOtp clientOtp,
        SendOtpInEmail sendOtpInEmail,
        SaveOtp saveOtp,
        VerifyOtp verifyOtp
        ) : ControllerBase
    {
        readonly CreateOtp _clientOtp = clientOtp;
        readonly SaveOtp _saveOtp = saveOtp;
        readonly SendOtpInEmail _sendOtpInEmail = sendOtpInEmail;
        readonly VerifyOtp _verifyOtp = verifyOtp;

        // teacher
        [HttpPost("teacher/verify-otp")]
        public ActionResult VerifyOtpForTeacher(OtpVerifyDto dto)
        {
            try
            {
                bool status = _verifyOtp.Verify(dto);
                if (status is false)
                {
                    Debug.WriteLine("invalid otp! : ");
                    return Unauthorized();
                }
                // success
                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"VerifyOtpForTeacher error: {ex}");
                return StatusCode(500);
            }
        }

        [HttpPost("teacher/send-email")]
        public ActionResult SendOtpToTeacher(OtpInEmailDto dto)
        {
            try
            {
                Totp totp = _clientOtp.Create(out byte[] secretKey, out string otp);

                _saveOtp.Save(dto.IdentificationNumber, secretKey);
                _sendOtpInEmail.Send(otp, dto.Username, dto.Email);

                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SendOtpToTeacher error: {ex}");
                return StatusCode(500);
            }
        }

        // client
        [HttpPost("student/verify-otp")]
        public ActionResult VerifyOtpForStudent(OtpVerifyDto dto)
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
                Debug.WriteLine($"VerifyOtpForStudent error: {ex}");
                return StatusCode(500);
            }
        }

        [HttpPost("student/send-email")]
        public ActionResult SendOtpToStudent(OtpInEmailDto dto)
        {
            try
            {
                Totp Totp = _clientOtp.Create(out byte[] secretKey, out string otp);
                _saveOtp.Save(dto.IdentificationNumber, secretKey);
                _sendOtpInEmail.Send(otp, dto.Username, dto.Email);

                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SendOtpToStudent error: {ex}");
                return StatusCode(500);
            }
        }
    }
}
