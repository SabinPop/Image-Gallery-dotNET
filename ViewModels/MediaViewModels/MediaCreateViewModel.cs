using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Image_Gallery.ViewModels.MediaViewModels
{
    public class MediaCreateViewModel
    {
        public List<IFormFile> Files { get; set; }
        public int CategoryId { get; set; }
    }
}
