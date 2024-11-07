using AutoMapper;
using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Helpers;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BadmintonBlast.DataAccess
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext context, IMapper mapper)
            : base(context, mapper) { }

        public async Task<IEnumerable<CustomerDTO>> GetAsync(
    string keyword,
    int pageIndex,
    int pageSize)
        {
            IQueryable<Customer> query = context.Set<Customer>();

            // Kiểm tra keyword có thuộc nhóm role hay không
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                // Chuyển từ khóa sang chữ thường để tìm kiếm không phân biệt chữ hoa chữ thường
                var lowerKeyword = keyword.ToLower();

                // Kiểm tra nếu từ khóa là "khách hàng", "nhân viên", hoặc "quản lý"
                if (lowerKeyword == "khách hàng" || lowerKeyword == "nhân viên" || lowerKeyword == "quản lý")
                {
                   // query = query.Where(customer => customer.role.ToLower() == lowerKeyword);
                }
                // Kiểm tra nếu từ khóa có chứa '@', xác định là email
                else if (keyword.Contains("@"))
                {
                    query = query.Where(customer => customer.Email.Contains(keyword));
                }
                // Nếu không thì tìm kiếm theo tên người dùng
                else
                {
                    query = query.Where(customer => customer.Namecustomer.Contains(keyword));
                }
            }

            // Phân trang
            query = query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

            // Trả về kết quả dưới dạng DTO
            return await mapper.ProjectTo<CustomerDTO>(query).ToListAsync();
        }


        public Task<int> GetTotalAsync(string keyword
            )
        {

            IQueryable<Customer> query = context.Set<Customer>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(c => c.Namecustomer.Contains(keyword));
            }
            return query.CountAsync();
        }
        public async Task<string> LoginAsync(string email, string password)
        {
            var customer = await context.Set<Customer>().FirstOrDefaultAsync(x => x.Email == email);

            if (customer == null || EncDscPassword.DecryptPassword(customer.PasswordHash) != password)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            // Create claims
            var claims = new[]
            {
        new Claim(ClaimTypes.Email, customer.Email),
        new Claim(ClaimTypes.NameIdentifier, customer.Idcustomer.ToString())
        // Add other claims if necessary
    };

            // Configure secret key (ensure it's at least 32 characters long)
            var secretKey = "badmintonblast@5864_AddSomeMoreCharacters";
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);

            if (keyBytes.Length < 32)
            {
                throw new Exception("The secret key must be at least 32 bytes (256 bits) long.");
            }

            var key = new SymmetricSecurityKey(keyBytes);

            // Create the token
            var token = new JwtSecurityToken(
                issuer: "https://localhost:7231/",
                audience: "Email",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            // Return the token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

