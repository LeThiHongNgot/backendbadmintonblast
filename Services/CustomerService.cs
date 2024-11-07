using AutoMapper;   
using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Helpers;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;
using BadmintonBlast.RequestModels;
using BadmintonBlast.Services.Adstraction;



namespace BadmintonBlast.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CustomerService(
            IUnitOfWork context,
            IWebHostEnvironment webHostEnvironment,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor
        )
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }


        public async Task DeleteAsync(int id)
        {
            // Lấy thông tin khách hàng để có đường dẫn hình ảnh
            var customer = await context.CustomerRepository.GetSingleAsync<CustomerDTO>(id);
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found");
            }

            // Xóa khách hàng từ cơ sở dữ liệu
            await context.CustomerRepository.DeleteAsync(id);
      
            await context.SaveChangesAsync();
        }

        public async Task<CustomerDTO> GetByIdAsync(int id)
        {
            // Xây dựng base URL cho hình ảnh
            var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

            // Lấy thông tin khách hàng từ repository
            var customer = await context.CustomerRepository.GetSingleAsync<Customer>(id);

            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found");
            }

            // Ánh xạ thông tin khách hàng vào DTO
            var customerDto = mapper.Map<Customer, CustomerDTO>(customer);

            // Cập nhật URL hình ảnh nếu có
            if (!string.IsNullOrEmpty(customerDto.ImageCustomer))
            {
                customerDto.ImageCustomer = $"{baseUrl}//{customer.ImageCustomer}";
            }

            return customerDto;
        }


        public async Task<IEnumerable<CustomerDTO>> GetCustomersAsync(GetCustomerRequest model)
        {
            var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

            var customers = await context.CustomerRepository.GetAsync(model.Keyword, model.PageIndex, model.PageSize);

            foreach (var customer in customers)
            {
                if (!string.IsNullOrEmpty(customer.ImageCustomer))
                {
                    customer.ImageCustomer = $"{baseUrl}//{customer.ImageCustomer}";
                }
            }

            return customers;
        }

        public Task<int> GetTotalCustomersAsync(GetTotalCustomerRequest model)
        {
            return context.CustomerRepository.GetTotalAsync(model.Keyword);
        }

        public async Task InsertAsync(UpdateCustomerRequest request)
        {
            try
            {
                if (!string.IsNullOrEmpty(request.PasswordHash))
                {
                    request.PasswordHash = EncDscPassword.EncryptPassword(request.PasswordHash);
                }

                string fileName = null;
                if (request.ImageCustomer != null)
                {
                    fileName = await SaveImage.SaveImageAsync(request.ImageCustomer, "wwwroot/assets/Customer/");
                }

                var entity = mapper.Map<UpdateCustomerRequest, Customer>(request);
                entity.ImageCustomer = fileName;

                await context.CustomerRepository.InsertAsync(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString()); 
                // You can also log this exception to a file or monitoring system
                throw;
            }
        }

    public async Task UpdateAsync(int id, UpdateCustomerRequest request)
{
    // Lấy thông tin khách hàng hiện tại từ cơ sở dữ liệu
    var existingCustomer = await context.CustomerRepository.GetSingleAsync(id);
    
    if (existingCustomer == null)
    {
        throw new KeyNotFoundException("Customer not found");
    }

    // Mã hóa mật khẩu nếu có
    if (!string.IsNullOrEmpty(request.PasswordHash))
    {
        request.PasswordHash = EncDscPassword.EncryptPassword(request.PasswordHash);
    }
    else
    {
        // Giữ nguyên mật khẩu hiện tại nếu không có mật khẩu mới
        request.PasswordHash = existingCustomer.PasswordHash;
    }

    // Xử lý hình ảnh nếu có
    string fileName = existingCustomer.ImageCustomer; // Giữ nguyên hình ảnh hiện tại mặc định
    if (request.ImageCustomer != null)
    {
        // Lưu hình ảnh mới và lấy tên file
        fileName = await SaveImage.SaveImageAsync(request.ImageCustomer, "wwwroot/assets/Customer/");
    }

    // Áp dụng các thay đổi từ request vào đối tượng hiện tại
    existingCustomer.Namecustomer = request.Namecustomer ?? existingCustomer.Namecustomer;
    existingCustomer.Phone = request.Phone ?? existingCustomer.Phone;
    existingCustomer.Province = request.Province ?? existingCustomer.Province;
    existingCustomer.District = request.District ?? existingCustomer.District;
    existingCustomer.Village = request.Village ?? existingCustomer.Village;
    existingCustomer.Hamlet = request.Hamlet ?? existingCustomer.Hamlet;
    existingCustomer.Email = request.Email ?? existingCustomer.Email;
    existingCustomer.PasswordHash = request.PasswordHash;
    existingCustomer.Status = request.Status ?? existingCustomer.Status;
    existingCustomer.Role = request.Role ?? existingCustomer.Role;
    existingCustomer.ImageCustomer = fileName;

    // Cập nhật khách hàng trong cơ sở dữ liệu
    context.CustomerRepository.Update(existingCustomer);
    await context.SaveChangesAsync();
}


        public async Task<string> LoginAsync(string email, string password)
        {
            return await context.CustomerRepository.LoginAsync(email,password);
        }
    }
}
