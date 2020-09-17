using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Image_Gallery.Infrastructure
{
    public interface IUnitOfWork
    {
        ICategoryRepo CategoryRepo { get; }
        IMediaRepo MediaRepo { get; }
        void Save();
        void UploadFile(List<IFormFile> files);
    }
}
