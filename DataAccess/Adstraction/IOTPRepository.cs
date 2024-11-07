using BadmintonBlast.Models.Entities;

namespace BadmintonBlast.DataAccess.Adstraction
{
    public interface IOTPRepository
    {
        Task<Otp> GenerateOtpAsync(string email);
        Task<bool> VerifyOtpAsync(string email, string otp);
        Task MarkOtpAsUsedAsync(string Email);
    }
}
