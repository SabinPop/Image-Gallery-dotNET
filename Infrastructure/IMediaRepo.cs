using Image_Gallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Image_Gallery.Infrastructure
{
    public interface IMediaRepo
    {
        List<Media> GetAll();
        Media GetById(int Id);
        void Insert(Media media);
        void Update(Media media);
        void Delete(int Id);
        void AddRange(List<Media> model);
        Media GetByCategory(Category category);
        List<Media> GetByCategoryId(int Id);
    }
}
