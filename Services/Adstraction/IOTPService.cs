using BadmintonBlast.Models.Entities;

namespace BadmintonBlast.Services.Adstraction
{
    public interface IOTPService
    {
        Task<Otp> GenerateOtpAsync(string email);
        Task<bool> VerifyOtpAsync(string email, string otp);
        Task MarkOtpAsUsedAsync(int idOtp);
    }
}
