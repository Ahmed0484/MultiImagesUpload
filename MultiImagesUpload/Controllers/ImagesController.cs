using Microsoft.AspNetCore.Mvc;
using MultiImagesUpload.Models;
using MultiImagesUpload.Repositories.Interface;

namespace MultiImagesUpload.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IUploadedImgRepo _uploadedImage;

        public ImagesController(IUploadedImgRepo uploadedImage)
        {
            _uploadedImage = uploadedImage;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UploadImage(List<IFormFile> files)
        {
            var uploadedImgs = new List<UploadedImage>();
            bool flag = false;
            foreach (var file in files)
            {
                ValidateFileUpload(file);

                if (ModelState.IsValid)
                {
                    // File upload
                    var uploadedImage = new UploadedImage
                    {
                        
                        FileName = file.FileName,
                       
                        DateCreated = DateTime.Now
                    };

                    uploadedImage = await _uploadedImage.Upload(file, uploadedImage);
                    uploadedImgs.Add(uploadedImage);
                    flag = true;

                    
                }
                else
                {
                    flag = false;
                }
            }
            if (flag) return Ok(uploadedImgs);
            else return View("Index");
        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file format");
            }

            if (file.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size cannot be more than 10MB");
            }
        }
    }
}
