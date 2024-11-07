namespace BadmintonBlast.Models.Dtos
{
    public class OrderDTO
    {
        public int Idorder { get; set; }

        public int Idbill { get; set; }

        public string? Idproduct { get; set; }

        public decimal? Price { get; set; }

        public string? Nameproduct { get; set; }

        public string? Color { get; set; }

        public string? Size { get; set; }

        public int? Quatity { get; set; }

        public DateTime? DateOrder { get; set; }
    }
}
