using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Models.Entities;
using BadmintonBlast.Models.Dtos;

namespace BadmintonBlast.DataAccess.Adstraction
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<ProductsDTO>> GetAsync(
          string keyword,
          int Idkindproduct,
          int pageIndex,
          int pageSize,
          int discount,
          int brand, decimal? minPrice,
          decimal? maxPrice  );

        Task<int> GetTotalAsync(string keyword, int discount,
            int? Idkindproduct,int brand, decimal? minPrice,
            decimal? maxPrice );
    }
}
