namespace BadmintonBlast.Models.Dtos
{
    public class BrandDTO
    {
        public int Idbrand { get; set; } 

        public string? Namebrand { get; set; }

        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
