using BadmintonBlast.Models.Dtos;
using BadmintonBlast.RequestModels;
using NuGet.Protocol.Plugins;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace BadmintonBlast.Services.Adstraction
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetCustomersAsync(GetCustomerRequest model);
        Task<int> GetTotalCustomersAsync(GetTotalCustomerRequest model);
        Task<CustomerDTO> GetByIdAsync(int id);
        Task InsertAsync(UpdateCustomerRequest model);
        Task UpdateAsync(int id, UpdateCustomerRequest model);
        Task DeleteAsync(int id);
        Task<string> LoginAsync(string email, string password);
    }
}



