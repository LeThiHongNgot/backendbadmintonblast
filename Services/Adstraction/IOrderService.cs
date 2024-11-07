using BadmintonBlast.Models.Dtos;
using BadmintonBlast.RequestModels;

namespace BadmintonBlast.Services.Adstraction
{
    public interface IOrderService
    {

        Task<IEnumerable<OrderDTO>> GetAsync();

        Task InsertAsync(OrderDTO brandDTO);
        Task<IEnumerable<OrderDTO>> GetByIdAsync(int id);
        Task UpdateAsync(int id, OrderDTO brandDTO);
        Task DeleteAsync(int id);
    }
}
