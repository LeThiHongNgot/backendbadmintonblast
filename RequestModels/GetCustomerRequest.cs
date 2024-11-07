namespace BadmintonBlast.RequestModels
{
    public class GetCustomerRequest : GetTotalCustomerRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class LoginRequestCustomer
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
