namespace BadmintonBlast.RequestModels
{
    public class UpdateBrandRequest
    {
        public  int Idbrand { get; set; } 

        public string? Namebrand { get; set; }

        public string? Description { get; set; }
    }
}
