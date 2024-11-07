using BadmintonBlast.Models.Dtos;
using BadmintonBlast.RequestModels;

namespace BadmintonBlast.Services.Adstraction
{
    public interface IPreviewService
    {
        Task<IEnumerable<PreviewDTO>> GetAsync(PageRequest page);

        Task InsertAsync(PreviewDTO pre);
        Task<int> GetTotalItemsAsync();
        Task<IEnumerable<PreviewDTO>> GetByIdAsync(int id);
        Task UpdateAsync(int id, PreviewDTO pre);
        Task DeleteAsync(int id);
    }
}
