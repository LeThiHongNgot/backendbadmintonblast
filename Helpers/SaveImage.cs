using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BadmintonBlast.Helpers
{
    public static class SaveImage
    {
        public static async Task<string> SaveImageAsync(IFormFile imageFile, string file)
        {
            if (imageFile != null)
            {
                // Lấy phần mở rộng của file
                var fileExtension = Path.GetExtension(imageFile.FileName);

                // Tạo tên file mới duy nhất bằng cách sử dụng Guid
                var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";

                // Đường dẫn lưu file (bao gồm tên file duy nhất)
                var filePath = Path.Combine(file, uniqueFileName);

                // Đảm bảo thư mục tồn tại
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                // Lưu file vào đường dẫn đã chỉ định
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Tạo đường dẫn tương đối từ thư mục wwwroot
                var relativePath = Path.GetRelativePath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), file);
                return Path.Combine(relativePath, uniqueFileName);
            }
            return null;
        }
    }

}
