using BadmintonBlast.Models.Dtos;
using BadmintonBlast.RequestModels;

namespace BadmintonBlast.Services.Adstraction
{
    public interface IProductService
    {

        Task<IEnumerable<ProductsDTO>> GetProductAsync(GetProductRequest model);
        Task<int> GetTotalProductAsync(GetTotalProductRequest model);
        Task<ProductsDTO> GetByIdAsync(int id);
        Task InsertAsync(UpdateProductImageRequest model);
        Task UpdateAsync(int id, UpdateProductImageRequest model );
        Task DeleteAsync(int id);

    }
}
