using AutoMapper;
using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Drawing.Drawing2D;


namespace BadmintonBlast.DataAccess
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ProductRepository(DbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
     : base(context, mapper)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<IEnumerable<ProductsDTO>> GetAsync(
      string keyword,
      int Idkindproduct,
      int discount,
      int pageIndex,
      int pageSize, int brand, decimal? minPrice, 
        decimal? maxPrice)
        {
            var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            IQueryable<Product> productQuery = context.Set<Product>();

            // Filter products by keyword
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                productQuery = productQuery.Where(product => product.Nameproduct.Contains(keyword));
            }

            // Filter products by kind product
            if (Idkindproduct != 0)
            {
                productQuery = productQuery.Where(product => product.Idkindproduct == Idkindproduct);
            }
            if (discount == 0)
            {
                // Lấy sản phẩm không giảm giá (Deprice == 0)
                productQuery = productQuery.Where(product => product.Deprice >= 0);
            }
            else if (discount == 1)
            {
                // Lấy tất cả sản phẩm (không quan tâm giảm giá)
                productQuery = productQuery.Where(product => product.Deprice > 0);
            }
            else if (discount > 2)
            {
                // Lấy sản phẩm có giảm giá với giá trị Deprice >= discount
                productQuery = productQuery.Where(product => product.Deprice == discount);
            }

            if (brand > 0)
            {
                productQuery = productQuery.Where(p => p.Idbrand== brand);
            }

            if (minPrice.HasValue)
            {
                productQuery = productQuery.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                productQuery = productQuery.Where(p => p.Price <= maxPrice.Value);
            }
            // Pagination for products
            productQuery = productQuery
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

            var products = await productQuery.ToListAsync();

            // Get related images and product stocks
            var images = await context.Set<Image>()
                .Where(image => products.Select(p => p.Idproduct).Contains(image.Idproduct))
                .ToListAsync();

            var productStocks = await context.Set<Productstock>()
                .Where(stock => products.Select(p => p.Idproduct).Contains(stock.Idproduct))
                .ToListAsync();

            var result = products.Select(product => new ProductsDTO
            {
                Idproduct = product.Idproduct,
                Idbrand = product.Idbrand,
                Idkindproduct = product.Idkindproduct,
                Nameproduct = product.Nameproduct,
                Kindproduct = product.Kindproduct,
                Namebrand = product.Namebrand,
                Description = product.Description,
                Price = product.Price,
                Available = product.Available,
                Deprice = product.Deprice,
                Date=product.Date,
                Image = images.Where(img => img.Idproduct == product.Idproduct)
                              .Select(img => new ImageDTO
                              {
                                  Idproduct = img.Idproduct,
                                  Image4 = $"{baseUrl}/{img.Image4}",  // Assuming Image4 is the primary image
                                  id=img.Id,
                              })
                              .ToList(),
                Productstocks = productStocks.Where(stock => stock.Idproduct == product.Idproduct)
                                              .Select(stock => new ProductStockDTO
                                              {
                                                  Id=stock.Id,
                                                  Idproduct = stock.Idproduct,
                                                  Namecolor = stock.Namecolor,
                                                  Namesize = stock.Namesize,
                                                  Quatity = stock.Quatity,
                                              })
                                              .ToList()
            });

            return result;
        }

        public async Task<int> GetTotalAsync(
      string keyword,
      int discount,
      int? Idkindproduct = null,
      int brand = 0, // Cung cấp giá trị mặc định cho brand
      decimal? minPrice = null, // Cung cấp giá trị mặc định
      decimal? maxPrice = null // Cung cấp giá trị mặc định
  )
        {
            IQueryable<Product> query = context.Set<Product>();

            // Kiểm tra điều kiện từ khóa
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(product => product.Nameproduct.Contains(keyword));
            }

            // Kiểm tra điều kiện Idkindproduct
            if (Idkindproduct.HasValue && Idkindproduct.Value != 0) // Kiểm tra giá trị nullable
            {
                query = query.Where(product => product.Idkindproduct == Idkindproduct.Value);
            }

            // Kiểm tra điều kiện discount
            if (discount > 0)
            {
                query = query.Where(product => product.Deprice >= discount);
            }

            // Kiểm tra điều kiện brand
            if (brand > 0)
            {
                query = query.Where(product => product.Idbrand == brand);
            }

            // Kiểm tra điều kiện minPrice
            if (minPrice.HasValue)
            {
                query = query.Where(product => product.Price >= minPrice.Value);
            }

            // Kiểm tra điều kiện maxPrice
            if (maxPrice.HasValue)
            {
                query = query.Where(product => product.Price <= maxPrice.Value);
            }

            // Trả về tổng số lượng sản phẩm thỏa mãn điều kiện
            return await query.CountAsync();
        }

    }
}
