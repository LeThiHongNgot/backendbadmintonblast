using BadmintonBlast.Models.Dtos;

namespace BadmintonBlast.Services.Adstraction
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDTO>> GetAsync();

        Task InsertAsync(ReservationDTO brandDTO);
        Task<IEnumerable<ReservationDTO>> GetByInvoiceIdAsync(int id);
        Task UpdateAsync(int id, ReservationDTO brandDTO);
        Task DeleteAsync(int id);


    }
}
