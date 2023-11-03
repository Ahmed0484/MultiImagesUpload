using MultiImagesUpload.Data;
using MultiImagesUpload.Models;
using MultiImagesUpload.Repositories.Interface;

namespace MultiImagesUpload.Repositories.Implementation
{
    public class UploadedImgRepo:IUploadedImgRepo
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _dbContext;

        public UploadedImgRepo(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            AppDbContext dbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }
        public async Task<UploadedImage> Upload(IFormFile file, UploadedImage uploadedImage)
        {
            // 1- Upload the Image to API/Images
            Guid x =Guid.NewGuid();
            uploadedImage.FileName =x+uploadedImage.FileName;
            var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{uploadedImage.FileName}");
            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            // 2-Update the database
            var httpRequest = _httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{uploadedImage.FileName}";
            uploadedImage.Url = urlPath;

            await _dbContext.UploadedImages.AddAsync(uploadedImage);
            await _dbContext.SaveChangesAsync();

            return uploadedImage;
        }
    }
}
