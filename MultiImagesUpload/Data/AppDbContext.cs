using Microsoft.EntityFrameworkCore;
using MultiImagesUpload.Models;

namespace MultiImagesUpload.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UploadedImage> UploadedImages { get; set; }
    }
}
