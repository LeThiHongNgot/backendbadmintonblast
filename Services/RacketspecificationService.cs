using AutoMapper;
using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;

namespace BadmintonBlast.Services
{
    public class RacketspecificationService : IRacketspecificationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RacketspecificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<RacketspecificationDTO>> GetAsync()
        {
            return await unitOfWork
            .RacketspecificationRepository
                .GetAsync<RacketspecificationDTO>();
        }

        public async Task<RacketspecificationDTO> GetByIdAsync(int id)
        {
            // Retrieve the RacketSpecification entity from the repository
            var racketSpecification = await unitOfWork
            .RacketspecificationRepository.GetSingleAsync<Racketspecification>(r => r.Idproduct == id);

            // Check if the racket specification was found
            if (racketSpecification == null)
            {
                return null; // or handle the not-found case as appropriate
            }

            // Map the RacketSpecification entity to RacketSpecificationDTO
            var racketSpecificationDto = mapper.Map<RacketspecificationDTO>(racketSpecification);

            return racketSpecificationDto;
        }

        public async Task InsertAsync(RacketspecificationDTO racketSpecificationDto)
        {
            {
                // Ensure mapping from DTO to Entity
                var entity = mapper.Map<Racketspecification>(racketSpecificationDto);
                await unitOfWork.RacketspecificationRepository.InsertAsync(entity);
                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(int id, RacketspecificationDTO racketSpecificationDto)
        {
            if (racketSpecificationDto == null)
            {
                throw new ArgumentNullException(nameof(racketSpecificationDto), "RacketSpecification data is required.");
            }

            var existingEntity = await unitOfWork.RacketspecificationRepository.GetSingleAsync<Racketspecification>(id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"RacketSpecification with ID {id} not found.");
            }

            // Map the updated values from RacketSpecificationDTO to the existing entity
            mapper.Map(racketSpecificationDto, existingEntity);

            // Update the entity in the repository
            unitOfWork.RacketspecificationRepository.Update(existingEntity);

            // Save changes asynchronously
            await unitOfWork.SaveChangesAsync();
        }

    }

}

