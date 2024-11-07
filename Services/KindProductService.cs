using AutoMapper;
using Azure.Core;
using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Helpers;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;
using BadmintonBlast.Services.Adstraction;
using Microsoft.IdentityModel.Tokens;


namespace BadmintonBlast.Services
{
    public class KindProductService : IKindProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        // Constructor với IHttpContextAccessor
        public KindProductService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor; // Khai báo IHttpContextAccessor
        }

        public async Task<IEnumerable<Kindproduct>> GetAsync()
        {
            // Build base URL for images
            var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

            // Fetch kind products from the repository
            var kindProducts = await unitOfWork.KindproductRepository.GetAsync();

            // Map to DTOs and convert to a list to allow modifications
            var kindProductDtos = mapper.Map<IEnumerable<Kindproduct>>(kindProducts).ToList();

            // Update image URL for each product
            foreach (var kindProductDto in kindProductDtos)
            {
                // Ensure that Image is not null or empty and update its URL
                if (!string.IsNullOrEmpty(kindProductDto.Image))
                {
                    // Assuming kindProductDto.Image contains the relative path (e.g., "images/product.jpg")
                    kindProductDto.Image = $"{baseUrl}/{kindProductDto.Image}";
                }
            }

            return kindProductDtos;
        }



        public async Task<KindProductDTO> GetByIdAsync(int id)
        {
            var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var kindProduct = await unitOfWork.KindproductRepository.GetSingleAsync(kp => kp.Idkindproduct == id);

            if (kindProduct == null)
            {
                return null; // or handle the not-found case as appropriate
            }
            kindProduct.Image = $"{baseUrl}/{kindProduct.Image}";
            return mapper.Map<KindProductDTO>(kindProduct);
        }

        public async Task InsertAsync(KindProductDTO kindProductDTO)
        {
            if (kindProductDTO == null)
            {
                throw new ArgumentNullException(nameof(kindProductDTO), "KindProduct data is required.");
            }

            string fileName = null;
            if (kindProductDTO.Image != null)
            {
                // Save the image and get the file name
                fileName = await SaveImage.SaveImageAsync(kindProductDTO.Image, "wwwroot/assets/Kindproduct/");
            }

            // Map the DTO to the Kindproduct entity
            var entity = mapper.Map<Kindproduct>(kindProductDTO);

            // Set the image file name in the entity (if an image was uploaded)
            if (fileName != null)
            {
                entity.Image = fileName;
            }
            
            // Insert the entity using the repository
            await unitOfWork.KindproductRepository.InsertAsync(entity);
            await unitOfWork.SaveChangesAsync();
            // Save the changes to the database

        }


        public async Task UpdateAsync(int id, KindProductDTO kindProductDTO)
        {
            if (kindProductDTO == null)
            {
                throw new ArgumentNullException(nameof(kindProductDTO), "KindProduct data is required.");
            }

            var existingEntity = await unitOfWork.KindproductRepository.GetSingleAsync(id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"KindProduct with ID {id} not found.");
            }

            string fileName = existingEntity.Image;

            if (kindProductDTO.Image != null)
            {
                fileName = await SaveImage.SaveImageAsync(kindProductDTO.Image, "wwwroot/assets/Kindproduct/");
            }

            existingEntity.Nameproduct = kindProductDTO.Nameproduct;
            existingEntity.Image = fileName;

            unitOfWork.KindproductRepository.Update(existingEntity);
            await unitOfWork.SaveChangesAsync();
        }



        public async Task DeleteAsync(int id)
        {
            await unitOfWork.KindproductRepository.DeleteAsync(id);
            await unitOfWork.SaveChangesAsync();
        }
    }
}

