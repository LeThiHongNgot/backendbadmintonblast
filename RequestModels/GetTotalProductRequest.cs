namespace BadmintonBlast.RequestModels
{
    public class GetTotalProductRequest
    {
            public string? keyword { get; set; }
            public int IdKindProduct { get; set; } 
            public int discount { get; set; }
            public int brandId { get; set; }
            public int? minPrice { get; set; }
            public int? maxPrice { get; set; }

    }
}
