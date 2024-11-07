using BadmintonBlast.Models.Dtos;

namespace BadmintonBlast.Services
{
    public interface IRacketspecificationService
    {
        Task<IEnumerable<RacketspecificationDTO>> GetAsync();

        Task InsertAsync(RacketspecificationDTO racketspecification);
        Task<RacketspecificationDTO> GetByIdAsync(int id);
        Task UpdateAsync(int id, RacketspecificationDTO brandDTO);

    }
}
