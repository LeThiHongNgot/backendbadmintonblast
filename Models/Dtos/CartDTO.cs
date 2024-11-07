namespace BadmintonBlast.Models.Dtos
{
    public class CartDTO
    {
        public int Idcart { get; set; } 

        public int Idproduct { get; set; } 

        public string? Idcustomer { get; set; }

        public int? Quatity { get; set; }
        public string? Color { get; set; }

        public string? Size { get; set; }

    }
}
