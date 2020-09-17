using Image_Gallery.Models;
using Microsoft.EntityFrameworkCore;
using Image_Gallery.ViewModels.MediaViewModels;
using Image_Gallery.ViewModels.CategoryViewModels;

namespace Image_Gallery.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<MediaViewModel> MediaViewModel { get; set; }
        public DbSet<CategoryViewModel> CategoryViewModel { get; set; }
        public DbSet<CreateCategoryViewModel> CreateCategoryViewModel { get; set; }
        public DbSet<EditCategoryViewModel> EditCategoryViewModel { get; set; }
        // public DbSet<MediaEditViewModel> MediaEditViewModel { get; set; }
        // public DbSet<MediaCreateViewModel> MediaCreateViewModel { get; set; }
    }
}
