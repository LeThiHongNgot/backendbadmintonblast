using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Models.Entities;
using Microsoft.EntityFrameworkCore;
using BadmintonBlast.Helpers;
using AutoMapper;

namespace BadmintonBlast.DataAccess
{
    public class OTPRepository : Repository<Customer>, IOTPRepository
    {
        public OTPRepository(DbContext context, IMapper mapper)
            : base(context, mapper) { }

        public async Task<Otp> GenerateOtpAsync(string email)
        {
            // Khởi tạo biến otp với giá trị null
            Otp otp = null;

            // Kiểm tra nếu OTP đã tồn tại cho email
            var existingOtp = await context.Set<Otp>()
                .Where(o => o.Email == email )
                .FirstOrDefaultAsync();

            if (existingOtp != null)
            {
                // Cập nhật thời gian và OTP nếu tồn tại
                existingOtp.Otp1 = GenerateRandomOtp(); // Hàm sinh OTP ngẫu nhiên
                existingOtp.CreatedAt = DateTime.UtcNow;
                existingOtp.Expiration = DateTime.UtcNow.AddMinutes(2); // Thay đổi thời gian hết hạn
                existingOtp.Attempts = 0;
                existingOtp.IsUsed = false;
                // Lưu thay đổi vào cơ sở dữ liệu
                context.Set<Otp>().Update(existingOtp);

                otp = existingOtp; // Gán giá trị cho otp
            }
            else
            {
                // Tạo mới OTP nếu không tồn tại
                otp = new Otp
                {
                    Email = email,
                    Otp1 = GenerateRandomOtp(), // Hàm sinh OTP ngẫu nhiên
                    CreatedAt = DateTime.UtcNow,
                    Expiration = DateTime.UtcNow.AddMinutes(2), // Thời gian hết hạn trong 2 phút
                    IsUsed = false,
                    Attempts = 0
                };

                // Thêm OTP mới vào cơ sở dữ liệu
                context.Set<Otp>().Add(otp);
            }

            await context.SaveChangesAsync();

            // Trả về OTP vừa tạo hoặc cập nhật
            return otp;
        }



        public async Task<bool> VerifyOtpAsync(string email, string otp)
        {
            var otpEntity = await context.Set<Otp>()
                .Where(o => o.Email == email && o.Otp1 == otp && !o.IsUsed && o.Expiration > DateTime.UtcNow)
                .FirstOrDefaultAsync();

            if (otpEntity != null)
            {
                otpEntity.Attempts += 1;
                if (otpEntity.Attempts >= 3)
                {
                    otpEntity.IsUsed = true;
                }

                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }


        public async Task MarkOtpAsUsedAsync(string Email)
        {
            var otp = await context.Set<Otp>().FindAsync(Email);
            if (otp != null)
            {
                otp.IsUsed = true;
                await context.SaveChangesAsync();
            }
        }

        private string GenerateRandomOtp()
        {
            // Tạo OTP ngẫu nhiên
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
