using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Image_Gallery.ViewModels.MediaViewModels
{
    public class MediaEditViewModel
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public IFormFile File { get; set; }
        public int CategoryId { get; set; }
    }
}
