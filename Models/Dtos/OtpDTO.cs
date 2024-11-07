namespace BadmintonBlast.Models.Dtos
{
    public class OtpDTO
    {
            public string Email { get; set; } = null!;
            public string Otp { get; set; } = null!;

    }

    public class EmailOTP
    {
        public string ToEmail { get; set; }
      
    }
}
