using AutoMapper;
using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;
using BadmintonBlast.RequestModels;
using BadmintonBlast.Services.Adstraction;
using Microsoft.AspNetCore.Mvc;

namespace BadmintonBlast.Services
{
    public class PreviewService : IPreviewService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PreviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<PreviewDTO>> GetAsync(PageRequest page)
        {
            return await unitOfWork
                .PreviewRepository.GetAsync<PreviewDTO>(page.PageIndex, page.PageSize);
        }

        public async Task<IEnumerable<PreviewDTO>> GetByIdAsync(int id)
        {
            // Retrieve a list of Preview entities from the repository
            var previews = await unitOfWork.PreviewRepository.GetAsync(p => p.Idproduct == id);

            // Map the list of Preview entities to a list of PreviewDTOs
            return mapper.Map<IEnumerable<PreviewDTO>>(previews);
        }


        public async Task InsertAsync(PreviewDTO previewDto)
        {
            var entity = mapper.Map<PreviewDTO, Preview>(previewDto);
            await unitOfWork.PreviewRepository.InsertAsync(entity);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<int> GetTotalItemsAsync()
        {
            return await unitOfWork.PreviewRepository.GetTotalItemsAsync();
        }
        public async Task UpdateAsync(int id, PreviewDTO previewDto)
        {
            if (previewDto == null)
            {
                throw new ArgumentNullException(nameof(previewDto), "Preview data is required.");
            }

            var existingEntity = await unitOfWork.PreviewRepository.GetSingleAsync(id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Preview with ID {id} not found.");
            }

            // Map the updated values from PreviewDTO to the existing entity
            mapper.Map(previewDto, existingEntity);

            // Update the entity in the repository
            unitOfWork.PreviewRepository.Update(existingEntity);

            // Save changes asynchronously
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await unitOfWork.PreviewRepository.DeleteAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

    }
}
