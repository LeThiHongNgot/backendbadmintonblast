namespace BadmintonBlast.RequestModels
{
    public class GetBillRequest: GetTotalBillRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class GetInvoiceRequest : GetTotalInvoiceRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

    }
}
