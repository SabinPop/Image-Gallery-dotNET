using Image_Gallery.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Image_Gallery.Infrastructure
{
    public interface ICategoryRepo
    {
        List<Category> GetAll();
        Category GetById(int Id);
        void Insert(Category category);
        void Update(Category category);
        void Delete(int Id);
        Task<List<Category>> GetAllAsync();
        Media RandomMediaByCategory(Category category);
        int NumberOfMedias(Category category);
    }
}
