namespace BadmintonBlast.RequestModels
{
    public class PaymentRequest
    { 
            public string Amount { get; set; }
            public string OrderInfo { get; set; }
            public string OrderInfor { get; set; }
            public string BankCode { get; set; } // Optional, can be null
        
    }
}
