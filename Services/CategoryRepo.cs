using Image_Gallery.Data;
using Image_Gallery.ExtensionMethods;
using Image_Gallery.Infrastructure;
using Image_Gallery.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Image_Gallery.Services
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly Context _context;


        public CategoryRepo(Context context)
        {
            _context = context;
        }

        public List<Category> GetAll() => _context.Categories.ToList();

        public async Task<List<Category>> GetAllAsync() => await _context.Categories.ToListAsync();

        public Category GetById(int Id) => _context.Categories.Where(x => x.Id.Equals(Id)).FirstOrDefault();

        public Media RandomMediaByCategory(Category category)
        {
            var numberOfMedias = NumberOfMedias(category);
            if (numberOfMedias > 0)
            {
                var media = _context.Media.Where(x => x.Category == category).Random();
                //Random random = new Random();
                // return (Media)media.OrderBy(_ => Guid.NewGuid()).Take(1);
                media.Category = category;
                media.CategoryId = category.Id;
                return media;
            }
            else
            {
                Media media = new Media()
                {
                    ImagePath = Path.Combine("uploads", "default_category.png"),
                    Category = category,
                    CategoryId = category.Id
                };
                _context.Media.Add(media);
                return media;
            }
           
        }

        public void Insert(Category category) => _context.Categories.Add(category);

        public void Update(Category category) => _context.Categories.Update(category);

        public void Delete(int Id)
        {
            var category = GetById(Id);
            _context.Categories.Remove(category);
        }

        public int NumberOfMedias(Category category)
        {
            return _context.Media.Where(x => x.Category == category)
                .Count();
        }
    }
}
