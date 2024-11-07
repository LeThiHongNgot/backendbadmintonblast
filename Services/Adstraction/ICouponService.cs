using BadmintonBlast.Models.Dtos;

namespace BadmintonBlast.Services.Adstraction
{
    public interface ICouponService
    {
        Task<IEnumerable<CouponDTO>> GetAsync();

        Task InsertAsync(CouponDTO cp);
        Task<CouponDTO> GetByIdAsync(int id);
        Task UpdateAsync(int id, CouponDTO cp);
        Task DeleteAsync(int id);
    }
}
