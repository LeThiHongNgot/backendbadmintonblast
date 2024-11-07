namespace BadmintonBlast.Models.Dtos
{
    public class CouponDTO
    {
        public int Idcoupon { get; set; } 
        public int? Promotion { get; set; }

        public DateTime? Startdate { get; set; }

        public DateTime? Enddate { get; set; }

        public int? Quality { get; set; }


    }
}
