namespace BadmintonBlast.RequestModels
{
    public class UpdateCustomerRequest
    {
        public int Idcustomer { get; set; }

        public string? Namecustomer { get; set; }

        public IFormFile? ImageCustomer { get; set; }

        public string? Phone { get; set; }

        public string? Province { get; set; }

        public string? District { get; set; }

        public string? Village { get; set; }

        public string? Hamlet { get; set; }

        public string? Email { get; set; } 

        public string? PasswordHash { get; set; }

        public bool? Status { get; set; }

        public string? Role { get; set; }

        public DateOnly? BirthDay { get; set; }
    }
}
