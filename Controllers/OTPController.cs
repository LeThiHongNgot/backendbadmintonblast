using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Helpers;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace BadmintonBlast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OTPController : ControllerBase
    {
        private readonly IOTPRepository _otpService;

        public OTPController(IOTPRepository otpService)
        {
            _otpService = otpService;
        }

        // Endpoint gửi OTP đến email
        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] EmailOTP emailDto)
        {
            // Kiểm tra xem email có hợp lệ hay không
            var validator = new CheckMail();
            if (!validator.IsValidEmail(emailDto.ToEmail))
            {
                return BadRequest(new { Message = "Invalid email address." });
            }

            try
            {
                // Tạo và gửi OTP
                var otp = await _otpService.GenerateOtpAsync(emailDto.ToEmail);

                // Cấu hình và gửi email OTP (ví dụ với SMTP)
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("blast5158@gmail.com", "iydv numx fwfh uhmq"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("blast5158@gmail.com", "Badminton Blast Support"),
                    Subject = "Mã OTP của bạn",
                    Body = $"Mã OTP của bạn là: <span style='color:blue;'>{otp.Otp1}</span>. Mã sẽ hết hạn sau 2 phút.",
                    IsBodyHtml = true, // Sử dụng HTML trong nội dung email
                };


                mailMessage.To.Add(emailDto.ToEmail);

                smtpClient.Send(mailMessage);

                return Ok(new { Status = "OTP sent successfully", Otp = otp.Otp1 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        // Endpoint xác thực OTP
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] OtpDTO otpDto)
        {
            var isValid = await _otpService.VerifyOtpAsync(otpDto.Email, otpDto.Otp);
            if (!isValid)
            {
                return BadRequest(new { Message = "Invalid or expired OTP." });
            }

            return Ok(new { Message = "OTP verified successfully." });
        }

        // Endpoint đánh dấu OTP đã sử dụng
        [HttpPost("mark-otp-used/{Email}")]
        public async Task<IActionResult> MarkOtpAsUsed(string Email)
        {
            await _otpService.MarkOtpAsUsedAsync(Email);
            return Ok(new { Message = "OTP marked as used." });
        }
    }
}
