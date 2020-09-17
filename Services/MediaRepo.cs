using Image_Gallery.Data;
using Image_Gallery.Infrastructure;
using Image_Gallery.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Image_Gallery.Services
{
    public class MediaRepo : IMediaRepo
    {
        public readonly Context _context;

        public MediaRepo(Context context) => _context = context;

        public void AddRange(List<Media> model) => _context.Media.AddRange(model);

        public void Delete(int Id)
        {
            var media = GetById(Id);
            _context.Media.Remove(media);
        }

        public List<Media> GetAll()
        {
            var data = _context.Media
                .Include(x => x.Category)
                .ToList();
            return data;
        }

        public Media GetByCategory(Category category)
        {
            return _context.Media.Find(category);
        }

        public List<Media>GetByCategoryId(int Id)
        {
            return _context.Media.Where(x => x.CategoryId == Id)
                .Include(x => x.Category)
                .ToList();
        }

        public Media GetById(int Id)
        {
            return _context.Media.Where(x => x.Id == Id)
                .Include(x => x.Category)
                .FirstOrDefault();
        }

        public void Insert(Media media) => _context.Media.Add(media);

        public void Update(Media media) => _context.Media.Update(media);
    }
}
