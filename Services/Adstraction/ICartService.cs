using BadmintonBlast.Models.Dtos;
using BadmintonBlast.RequestModels;

namespace BadmintonBlast.Services.Adstraction
{
    public interface ICartService
    {
        Task<IEnumerable<CartDTO>> GetAsync(PageRequest page);

        Task InsertAsync(CartDTO pre);
        Task<int> GetTotalItemsAsync();
        Task<IEnumerable<CartDTO>> GetByIdAsync(int id);
        Task UpdateAsync(int id, CartDTO pre);
        Task DeleteAsync(int id);
    }
}
