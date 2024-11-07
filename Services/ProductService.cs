using AutoMapper;
using BadmintonBlast.DataAccess;
using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Helpers;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;
using BadmintonBlast.RequestModels;
using BadmintonBlast.Services.Adstraction;
using Microsoft.AspNetCore.Http;

namespace BadmintonBlast.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ProductService(
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
            // Lấy thông tin sản phẩm từ database
            var product = await context.ProductRepository.GetSingleAsync<Product>(id);
            var image = await context.ImageRepository.GetSingleAsync<Image>(id);
            var productStocks = context.ProductStockRepository.GetAll()
                                    .Where(ps => ps.Idproduct == id)
                                    .ToList();

            if (product != null)
            {
                // Xóa hình ảnh liên quan nếu có
                if (image != null && !string.IsNullOrEmpty(image.Image4))
                {
                    var images = new[] { image.Image4 };
                    var contentRootPath = webHostEnvironment.ContentRootPath;
                    var imageFolderPath = Path.Combine(contentRootPath, "wwwroot", "images", "Product");

                    foreach (var img in images)
                    {
                        if (!string.IsNullOrEmpty(img))
                        {
                            var filePath = Path.Combine(imageFolderPath, img);

                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);
                            }
                        }
                    }
                }

                // Xóa tất cả các sản phẩm tồn kho liên quan
                if (productStocks.Any())
                {
                    foreach(var i in  productStocks)
                    {
                        context.ProductStockRepository.DeleteAsync(i);
                    }    
                   
                }

                // Xóa hình ảnh liên quan
                if (image != null)
                {
                    await context.ImageRepository.DeleteAsync(image);
                }

                // Xóa sản phẩm từ database
                await context.ProductRepository.DeleteAsync(product);

                // Lưu các thay đổi
                await context.SaveChangesAsync();
            }
        }

        public async Task<ProductsDTO> GetByIdAsync(int id)
        {
            // Xây dựng base URL cho hình ảnh
            var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

            // Lấy thông tin sản phẩm từ cơ sở dữ liệu
            var product = await context.ProductRepository.GetSingleAsync<Product>(id);

            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            // Lấy danh sách Productstock có Idproduct tương ứng
            var productStocks = await context.ProductStockRepository.GetAsync<Productstock>(x => x.Idproduct == id);

            // Lấy danh sách hình ảnh liên quan cho sản phẩm đó
            var images = await context.ImageRepository.GetAsync<Image>(x => x.Idproduct == id);

            // Tạo DTO và ánh xạ dữ liệu từ sản phẩm
            var productDto = mapper.Map<Product, ProductsDTO>(product);

            // Cập nhật danh sách hình ảnh trong DTO nếu có
            if (images != null && images.Any())
            {
                productDto.Image = images.Select(img => new ImageDTO
                {
                    Idproduct = img.Idproduct,
                    Image4 = !string.IsNullOrEmpty(img.Image4) ? $"{baseUrl}/{img.Image4}" : null,
                    id =img.Id,
                }).ToList();
            }

            // Ánh xạ danh sách Productstock sang ProductstockDTO và gán vào ProductsDTO
            productDto.Productstocks = productStocks.Select(stock => mapper.Map<Productstock, ProductStockDTO>(stock)).ToList();

            return productDto;
        }


        public async Task<IEnumerable<ProductsDTO>> GetProductAsync(GetProductRequest model)
        {

            return await context.ProductRepository.GetAsync(model.keyword, model.IdKindProduct,model.discount, model.PageIndex, model.PageSize,model.brandId,model.minPrice,model.maxPrice);
        }

        public Task<int> GetTotalProductAsync(GetTotalProductRequest model)
        {
            return context.ProductRepository.GetTotalAsync(model.keyword, model.IdKindProduct,model.discount,model.brandId,model.minPrice,model.maxPrice);
        }


        public async Task InsertAsync(UpdateProductImageRequest request)
        {
            // Tạo một thực thể Product sử dụng các thuộc tính từ yêu cầu
            var productEntity = new Product
            {
                Idbrand = request.Idbrand,
                Idkindproduct = request.Idkindproduct,
                Nameproduct = request.Nameproduct,
                Kindproduct = request.Kindproduct,
                Namebrand = request.Namebrand,
                Description = request.Description,
                Price = request.Price,
                Available = request.Available,
                Deprice = request.Deprice
            };

            // Chèn sản phẩm vào cơ sở dữ liệu và lưu lại để nhận ID sinh ra
            await context.ProductRepository.InsertAsync(productEntity);
            await context.SaveChangesAsync();

            // Lấy Id của sản phẩm vừa tạo
            var productId = productEntity.Idproduct;

            // Kiểm tra xem danh sách hình ảnh có tồn tại không và lưu từng hình ảnh
            if (request.Image != null && request.Image.Any())
            {
                foreach (var imageFile in request.Image)
                {
                    var imageEntity = new Image
                    {
                        Idproduct = productId, // Liên kết hình ảnh với sản phẩm vừa tạo
                        Image4 = await SaveImage.SaveImageAsync(imageFile, "wwwroot/asserts/Product/")
                    };

                    // Chèn hình ảnh vào cơ sở dữ liệu
                    await context.ImageRepository.InsertAsync(imageEntity);
                }

                // Lưu tất cả thay đổi cho các hình ảnh
                await context.SaveChangesAsync();
            }
           
        }


        public async Task UpdateAsync(int id, UpdateProductImageRequest request)
        {
            // Retrieve the existing Product from the database
            var existingProduct = await context.ProductRepository.GetSingleAsync<Product>(id);

            if (existingProduct == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            // Update the Product entity with the new values from the request
            existingProduct.Idbrand = request.Idbrand;
            existingProduct.Idkindproduct = request.Idkindproduct;
            existingProduct.Nameproduct = request.Nameproduct;
            existingProduct.Kindproduct = request.Kindproduct;
            existingProduct.Namebrand = request.Namebrand;
            existingProduct.Description = request.Description;
            existingProduct.Price = request.Price;
            existingProduct.Available = request.Available;
            existingProduct.Deprice = request.Deprice;

            // Update the Product in the database
            context.ProductRepository.Update(existingProduct);

            // Xử lý cập nhật danh sách hình ảnh (nếu có)
            if (request.Image != null && request.Image.Any())
            {
                // Xóa các hình ảnh cũ liên quan đến sản phẩm
                var existingImages = await context.ImageRepository.GetAsync(i => i.Idproduct == id);
                foreach (var image in existingImages)
                {
                    context.ImageRepository.DeleteAsync(image);
                }

                // Lưu lại các hình ảnh mới
                foreach (var imageFile in request.Image)
                {
                    var newImage = new Image
                    {
                        Idproduct = id,
                        Image4 = await SaveImage.SaveImageAsync(imageFile, "wwwroot/asserts/Product/")
                    };

                    await context.ImageRepository.InsertAsync(newImage);
                }
            }         

            // Save all changes to the database
            await context.SaveChangesAsync();
        }



    }
}
