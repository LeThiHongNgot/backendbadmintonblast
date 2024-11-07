namespace BadmintonBlast.Models.Dtos
{
    public class HourlyRateDTO
    {
        public int Idhourlyrates { get; set; } 

        public TimeOnly? Starttimerates { get; set; }

        public TimeOnly? Endtimerates { get; set; }

        public decimal? Price { get; set; }
    }
}
