using AutoMapper;
using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;
using BadmintonBlast.RequestModels;
using BadmintonBlast.Services.Adstraction;

namespace BadmintonBlast.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CartDTO>> GetAsync(PageRequest page)
        {
            return await unitOfWork
                .CartRepository.GetAsync<CartDTO>(page.PageIndex, page.PageSize);
        }

        public async Task<IEnumerable<CartDTO>> GetByIdAsync(int id)
        {
            var cart = await unitOfWork.CartRepository.GetAsync<Cart>(c => c.Idcustomer == id);

            if (cart == null)
            {
                return null; // or handle the not-found case as appropriate
            }
            return mapper.Map<IEnumerable<CartDTO>>(cart);
        }

        public async Task InsertAsync(CartDTO cartDto)
        {
            var entity = mapper.Map<CartDTO, Cart>(cartDto);
            await unitOfWork.CartRepository.InsertAsync(entity);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<int> GetTotalItemsAsync()
        {
            return await unitOfWork.CartRepository.GetTotalItemsAsync();
        }

        public async Task UpdateAsync(int id, CartDTO cartDto)
        {
            if (cartDto == null)
            {
                throw new ArgumentNullException(nameof(cartDto), "Cart data is required.");
            }

            var existingEntity = await unitOfWork.CartRepository.GetSingleAsync(id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Cart with ID {id} not found.");
            }

            mapper.Map(cartDto, existingEntity);
            unitOfWork.CartRepository.Update(existingEntity);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await unitOfWork.CartRepository.DeleteAsync(id);
            await unitOfWork.SaveChangesAsync();
        }
    }
}

