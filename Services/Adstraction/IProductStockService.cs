using BadmintonBlast.Models.Dtos;

namespace BadmintonBlast.Services.Adstraction
{
    public interface IProductStockService
    {
        Task<IEnumerable<ProductStockDTO>> GetAllAsync(); // Lấy tất cả sản phẩm trong kho
        Task<ProductStockDTO> GetByIdAsync(int id); // Lấy thông tin sản phẩm trong kho theo id
        Task<IEnumerable<ProductStockDTO>> GetByProductIdAsync(int productId); // Lấy thông tin sản phẩm trong kho theo id sản phẩm
        Task InsertAsync(ProductStockDTO productStockDTO); // Thêm thông tin sản phẩm vào kho
        Task UpdateAsync(int id, ProductStockDTO productStockDTO); // Cập nhật thông tin sản phẩm trong kho
        Task DeleteAsync(int id); // Xóa thông tin sản phẩm trong kho theo id
        Task DeleteByProductIdAsync(int productId);
    }
}
