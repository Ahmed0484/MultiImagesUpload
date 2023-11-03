using MultiImagesUpload.Models;

namespace MultiImagesUpload.Repositories.Interface
{
    public interface IUploadedImgRepo
    {
        Task<UploadedImage> Upload(IFormFile file, UploadedImage uploadedImage);
    }
}
