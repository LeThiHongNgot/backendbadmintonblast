using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Models.Entities;

namespace BadmintonBlast.Services
{
    public class OTPService : IOTPRepository
    {
        private readonly IOTPRepository _otpRepository;

        public OTPService(IOTPRepository otpRepository)
        {
            _otpRepository = otpRepository;
        }

        public async Task<Otp> GenerateOtpAsync(string email)
        {
            return await _otpRepository.GenerateOtpAsync(email);
        }

        public async Task<bool> VerifyOtpAsync(string email, string otp)
        {
            var otpEntity = await _otpRepository.VerifyOtpAsync(email, otp);
            return otpEntity != null;
        }

        public async Task MarkOtpAsUsedAsync(string Email)
        {
            await _otpRepository.MarkOtpAsUsedAsync(Email);
        }
    }
}
