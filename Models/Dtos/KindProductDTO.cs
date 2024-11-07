namespace BadmintonBlast.Models.Dtos
{
    public class KindProductDTO
    {
        public int Idkindproduct { get; set; }

        public string? Nameproduct { get; set; }
        public IFormFile? Image { get; set; }
    }
}
