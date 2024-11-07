using BadmintonBlast.Models.Dtos;

namespace BadmintonBlast.RequestModels
{
    public class GetProductRequest :GetTotalProductRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class UpdateProductRequest 
    {
        public  int Idproduct { get; set; }

        public int? Idbrand { get; set; }

        public int Idkindproduct { get; set; }

        public string? Nameproduct { get; set; }

        public string? Kindproduct { get; set; }

        public string? Namebrand { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public byte? Available { get; set; }

        public int? Deprice { get; set; }

        public DateTime? Date { get; set; }
    }

    public class UpdateImageRequest
    {
        public int Idproduct { get; set; }

        public IFormFile? Image4 { get; set; }

        public int Id { get; set; }
    }

    public class UpdateProductImageRequest      
    {

        public int Idproduct { get; set; }

        public int? Idbrand { get; set; }

        public int? Idkindproduct { get; set; }

        public string? Nameproduct { get; set; }

        public string? Kindproduct { get; set; }

        public string? Namebrand { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public byte? Available { get; set; }

        public int? Deprice { get; set; }
        public DateTime? Date { get; set; }

        public List<IFormFile>? Image { get; set; }

        //public List<ProductStockDTO>? Productstocks { get; set; }

    }

    public class ProductstockRequest
    {
        public int Idproduct { get; set; } 

        public string? Namecolor { get; set; }

        public string? Namesize { get; set; }

        public int? Quatity { get; set; }

    }
}
