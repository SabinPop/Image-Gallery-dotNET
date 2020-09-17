using Image_Gallery.Data;
using Image_Gallery.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Image_Gallery.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;
        private ICategoryRepo _categoryRepo;
        private IMediaRepo _mediaRepo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UnitOfWork(Context context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public ICategoryRepo CategoryRepo
        {
            get
            {
                return _categoryRepo ??= new CategoryRepo(_context);
            }
        }

        public IMediaRepo MediaRepo
        {
            get
            {
                return _mediaRepo ??= new MediaRepo(_context);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async void UploadFile(List<IFormFile> files)
        {
            foreach(IFormFile item in files)
            {
                long totalBytes = files.Sum(f => f.Length);
                string filename = item.FileName.Trim('"');
                filename = EnsureFileName(filename);
                byte[] buffer = new byte[16 * 1024];
                using FileStream output = File.Create(GetPathAndFileName(filename));
                using Stream input = item.OpenReadStream();
                int readBytes;
                while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    await output.WriteAsync(buffer, 0, readBytes);
                    totalBytes += readBytes;
                }
            }
        }

        private string GetPathAndFileName(string filename)
        {
            var path = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var fullPath = Path.Combine(path, filename);
            return fullPath;
        }

        private string EnsureFileName(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);
            return filename;
        }
    }
}
