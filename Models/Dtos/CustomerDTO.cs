namespace BadmintonBlast.Models.Dtos
{
    public class CustomerDTO
    {
        public int Idcustomer { get; set; }

        public string? Namecustomer { get; set; }

        public string? ImageCustomer { get; set; }

        public string? Phone { get; set; }

        public string? Province { get; set; }

        public string? District { get; set; }

        public string? Village { get; set; }

        public string? Hamlet { get; set; }

        public string Email { get; set; } = null!;

        public string? PasswordHash { get; set; }

        public bool? Status { get; set; }

        public string? Role { get; set; }
        public DateOnly? Birthday { get; set; }
    }
}
