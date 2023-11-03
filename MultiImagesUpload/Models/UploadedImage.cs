namespace MultiImagesUpload.Models
{
    public class UploadedImage
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
