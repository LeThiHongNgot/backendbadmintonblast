using AutoMapper;
using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;
using BadmintonBlast.Services.Adstraction;

namespace BadmintonBlast.Services
{
    public class CouponService : ICouponService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CouponService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CouponDTO>> GetAsync()
        {
            return await unitOfWork
                .CouponRepository
                .GetAsync<CouponDTO>();
        }

        public async Task<CouponDTO> GetByIdAsync(int  id)
        {
            // Retrieve the Coupon entity from the repository
            var coupon = await unitOfWork.CouponRepository.GetSingleAsync<Coupon>(c => c.Idcoupon == id);

            // Check if the coupon was found
            if (coupon == null)
            {
                return null; // or handle the not-found case as appropriate
            }

            // Map the Coupon entity to CouponDTO
            var couponDto = mapper.Map<CouponDTO>(coupon);

            return couponDto;
        }

        public async Task InsertAsync(CouponDTO coupon)
        {
            var entity = mapper.Map<CouponDTO, Coupon>(coupon);
            await unitOfWork.CouponRepository.InsertAsync(entity);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CouponDTO couponDTO)
        {
            if (couponDTO == null)
            {
                throw new ArgumentNullException(nameof(couponDTO), "Coupon data is required.");
            }

            var existingEntity = await unitOfWork.CouponRepository.GetSingleAsync(id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Coupon with ID {id} not found.");
            }

            // Map the updated values from CouponDTO to the existing entity
            mapper.Map(couponDTO, existingEntity);

            // Update the entity in the repository
            unitOfWork.CouponRepository.Update(existingEntity);

            // Save changes asynchronously
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await unitOfWork.CouponRepository.DeleteAsync(id);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
