using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;

namespace BadmintonBlast.Services.Adstraction
{
    public interface IKindProductService
    {
        Task<IEnumerable<Kindproduct>> GetAsync();

        Task InsertAsync(KindProductDTO KPDTO);
        Task<KindProductDTO> GetByIdAsync(int id);
        Task UpdateAsync(int id, KindProductDTO KPDTO);
        Task DeleteAsync(int id);
    }
}   
