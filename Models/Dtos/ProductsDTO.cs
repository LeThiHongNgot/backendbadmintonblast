namespace BadmintonBlast.Models.Dtos
{
    public class ProductsDTO
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

        public List<ImageDTO>? Image{ get; set; }

        public List<ProductStockDTO>? Productstocks { get; set; }

    }
}
