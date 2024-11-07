using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Models.Entities;
using BadmintonBlast.Models.Dtos;

namespace BadmintonBlast.DataAccess.Adstraction
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<CustomerDTO>> GetAsync(
              string keyword,
              int pageIndex,
              int pageSize);

       Task<int> GetTotalAsync(string keyword );

       Task<string> LoginAsync(string email, string password);

       

    }
}
