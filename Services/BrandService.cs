using AutoMapper;
using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Helpers;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;
using BadmintonBlast.Services.Adstraction;

namespace BadmintonBlast.Services
{
    public class BrandService : IBrandService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        // Constructor với IHttpContextAccessor
        public BrandService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor; // Khai báo IHttpContextAccessor
        }

            public async Task<IEnumerable<Brand>> GetAsync()
            {
                var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

                // Lấy dữ liệu từ repository
                var brands = await unitOfWork.BrandRepository.GetAsync();

                // Giả sử BrandDTO có một thuộc tính ImageUrl cần được bổ sung base URL
                var Brand = mapper.Map<IEnumerable<Brand>>(brands).ToList();

                // Update image URL for each product
                foreach (var brand in Brand)
                {
                    // Ensure that Image is not null or empty and update its URL
                    if (!string.IsNullOrEmpty(brand.Image))
                    {
                        brand.Image = $"{baseUrl}/{brand.Image}";
                    }
                }

                return brands;
            }


            public async Task<BrandDTO> GetByIdAsync(int id)
            {
                var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
                // Retrieve the Brand entity from the repository
                var brand = await unitOfWork.BrandRepository.GetSingleAsync<Brand>(b => b.Idbrand == id);

                // Check if the brand was found
                if (brand == null)
                {
                    return null; // or handle the not-found case as appropriate
                }
                brand.Image = $"{baseUrl}/{brand.Image}";
                // Map the Brand entity to BrandDTO
                var brandDto = mapper.Map<BrandDTO>(brand);

                return brandDto;
            }

        public async Task InsertAsync(BrandDTO brandDTO)
        {
            if (brandDTO == null)
            {
                throw new ArgumentNullException(nameof(brandDTO), "Brand data is required.");
            }

            string fileName = null;
            if (brandDTO.Image != null)
            {
                // Save the image and get the file name
                fileName = await SaveImage.SaveImageAsync(brandDTO.Image, "wwwroot/assets/Brand/");
            }

            // Map the DTO to the Brand entity
            var entity = mapper.Map<Brand>(brandDTO);

            // Set the image file name in the entity (if an image was uploaded)
            if (fileName != null)
            {
                entity.Image = fileName;
            }

            // Insert the entity using the repository
            await unitOfWork.BrandRepository.InsertAsync(entity);
            await unitOfWork.SaveChangesAsync();
            // Save the changes to the database
        }

        public async Task UpdateAsync(int id, BrandDTO brandDTO)
        {
            if (brandDTO == null)
            {
                throw new ArgumentNullException(nameof(brandDTO), "Brand data is required.");
            }

            var existingEntity = await unitOfWork.BrandRepository.GetSingleAsync(id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Brand with ID {id} not found.");
            }

            string fileName = existingEntity.Image;

            if (brandDTO.Image != null)
            {
                // Save the image and get the file name
                fileName = await SaveImage.SaveImageAsync(brandDTO.Image, "wwwroot/assets/Brand/");
            }

            // Update the necessary properties
            existingEntity.Image = fileName;

            // Update the entity using the repository
            unitOfWork.BrandRepository.Update(existingEntity);
            await unitOfWork.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            await unitOfWork.BrandRepository.DeleteAsync(id);
            await unitOfWork.SaveChangesAsync();
        }


    }
}
