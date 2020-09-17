using Image_Gallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Image_Gallery.ViewModels.CategoryViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Media CategoryMedia { get; set; } = new Media();

    }
}
