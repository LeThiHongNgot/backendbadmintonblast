using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;


namespace BadmintonBlast.Services.Adstraction
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetAsync();

        Task InsertAsync(BrandDTO brandDTO);
        Task<BrandDTO> GetByIdAsync(int id);
        Task UpdateAsync(int id , BrandDTO brandDTO);
        Task DeleteAsync(int id);

        
    }
}
