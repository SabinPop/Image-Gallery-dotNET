using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Image_Gallery.Infrastructure;
using Image_Gallery.Models;
using Image_Gallery.ViewModels.MediaViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Image_Gallery.Controllers
{
    public class MediaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MediaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            var media = _unitOfWork.MediaRepo.GetAll();
            var viewModel = _mapper.Map<List<MediaViewModel>>(media);
            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var media = _unitOfWork.MediaRepo.GetById(id);
            var viewModel = _mapper.Map<MediaViewModel>(media);
            return View(viewModel);
        }

        public ActionResult Create()
        {
            ViewBag.Categories = _unitOfWork.CategoryRepo.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MediaCreateViewModel viewModel)
        {
            try
            {
                var category = _unitOfWork.CategoryRepo.GetById(viewModel.CategoryId);
                List<Media> media = new List<Media>();
                foreach(var file in viewModel.Files)
                {
                    media.Add(new Media()
                    {
                        ImagePath = Path.Combine("uploads", file.FileName).ToString(),
                        Category = category
                    });
                }
                _unitOfWork.UploadFile(viewModel.Files);
                _unitOfWork.MediaRepo.AddRange(media);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Categories = _unitOfWork.CategoryRepo.GetAll();
            var media = _unitOfWork.MediaRepo.GetById(id);
            var mappedMedia = _mapper.Map<MediaEditViewModel>(media);
            return View(mappedMedia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MediaEditViewModel viewModel)
        {
            try
            {
                if(viewModel.File == null && _unitOfWork.MediaRepo.GetById(viewModel.Id).CategoryId == viewModel.CategoryId)
                {
                    RedirectToAction(nameof(Index));
                }
                else if(viewModel.File != null)
                {
                    List<IFormFile> files = new List<IFormFile>
                    {
                        viewModel.File
                    };
                    var updateMedia = _unitOfWork.MediaRepo.GetById(viewModel.Id);
                    updateMedia.CategoryId = viewModel.CategoryId;
                    updateMedia.ImagePath = viewModel.File.FileName;

                    _unitOfWork.UploadFile(files);
                    _unitOfWork.MediaRepo.Update(updateMedia);
                    _unitOfWork.Save();
                    RedirectToAction(nameof(Index));
                }
                else if(_unitOfWork.MediaRepo.GetById(viewModel.Id).CategoryId != viewModel.CategoryId)
                {
                    List<IFormFile> files = new List<IFormFile>
                    {
                        viewModel.File
                    };
                    var updateMedia = _unitOfWork.MediaRepo.GetById(viewModel.Id);
                    updateMedia.CategoryId = viewModel.CategoryId;
                    updateMedia.ImagePath = _unitOfWork.MediaRepo.GetById(viewModel.Id).ImagePath;

                    _unitOfWork.UploadFile(files);
                    _unitOfWork.Save();
                    RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var model = _unitOfWork.MediaRepo.GetById(id);
            var viewModel = _mapper.Map<MediaViewModel>(model);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var model = _unitOfWork.MediaRepo.GetById(id);
                if (model == null)
                    return NotFound();
                _unitOfWork.MediaRepo.Delete(id);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
