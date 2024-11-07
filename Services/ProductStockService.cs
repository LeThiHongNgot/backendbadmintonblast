using AutoMapper;
using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;
using BadmintonBlast.Services.Adstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BadmintonBlast.Services
{
    public class ProductStockService : IProductStockService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductStockService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // Lấy tất cả sản phẩm trong kho
        public async Task<IEnumerable<ProductStockDTO>> GetAllAsync()
        {
            var productStocks = await unitOfWork.ProductStockRepository.GetAsync();
            return mapper.Map<IEnumerable<ProductStockDTO>>(productStocks);
        }

        // Lấy sản phẩm trong kho theo id
        public async Task<ProductStockDTO> GetByIdAsync(int id)
        {
            var productStock = await unitOfWork.ProductStockRepository.GetSingleAsync(ps => ps.Id == id);
            if (productStock == null)
            {
                return null;
            }
            return mapper.Map<ProductStockDTO>(productStock);
        }

        // Lấy sản phẩm trong kho theo id sản phẩm
        public async Task<IEnumerable<ProductStockDTO>> GetByProductIdAsync(int productId)
        {
            var productStocks = await unitOfWork.ProductStockRepository.GetAsync(ps => ps.Idproduct== productId);
            return mapper.Map<IEnumerable<ProductStockDTO>>(productStocks);
        }
            
        // Thêm sản phẩm vào kho
        public async Task InsertAsync(ProductStockDTO productStockDTO)
        {
            var productStock = mapper.Map<Productstock>(productStockDTO);
            await unitOfWork.ProductStockRepository.InsertAsync(productStock);
            await unitOfWork.SaveChangesAsync();
        }

        // Cập nhật sản phẩm trong kho
        public async Task UpdateAsync(int id, ProductStockDTO productStockDTO)
        {
            var existingProductStock = await unitOfWork.ProductStockRepository.GetSingleAsync(ps => ps.Id == id);
            if (existingProductStock == null)
            {
                throw new KeyNotFoundException($"ProductStock with ID {id} not found.");
            }

            mapper.Map(productStockDTO, existingProductStock);
            unitOfWork.ProductStockRepository.Update(existingProductStock);
            await unitOfWork.SaveChangesAsync();
        }

        // Xóa sản phẩm trong kho theo id
        public async Task DeleteAsync(int id)
        {
            await unitOfWork.ProductStockRepository.DeleteAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

        // Xóa sản phẩm trong kho theo id sản phẩm
        public async Task DeleteByProductIdAsync(int productId)
        {
            var productStocks = await unitOfWork.ProductStockRepository.GetAsync(ps => ps.Idproduct == productId);
            foreach (var productStock in productStocks)
            {
                unitOfWork.ProductStockRepository.DeleteAsync(productStock);
            }
            await unitOfWork.SaveChangesAsync();
        }
    }
}
