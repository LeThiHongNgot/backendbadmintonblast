using System.Diagnostics.Eventing.Reader;

namespace BadmintonBlast.RequestModels
{
    public class GetTotalBillRequest
    {
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int status { get; set; }
        public string?           Keyword {get;set;}
    }
    public class GetTotalInvoiceRequest
    { 
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        
        public string? CustomerName { get; set; }
    }
}
