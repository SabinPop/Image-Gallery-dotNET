using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Image_Gallery.ExtensionMethods;
using Image_Gallery.Infrastructure;
using Image_Gallery.Models;
using Image_Gallery.ViewModels.CategoryViewModels;
using Image_Gallery.ViewModels.MediaViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Image_Gallery.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [Route("/categories")]
        public ActionResult Index()
        {
            List<Category> categories = _unitOfWork.CategoryRepo.GetAll();
            List<Media> medias = new List<Media>();
            foreach(var category in categories)
            {
                medias.Add(_unitOfWork.CategoryRepo.RandomMediaByCategory(category));
            }
            List<CategoryViewModel> mappedCategories = _mapper.Map<List<CategoryViewModel>>(medias);
            return View(mappedCategories);
        }

        public ActionResult Details(int id)
        {
            var category = _unitOfWork.CategoryRepo.GetById(id);
            var mappedCategory = _mapper.Map<CategoryViewModel>(category);
            return View(mappedCategory);
        }

        [Route("/categories/{id:int}")]
        public ActionResult Medias(int id)
        {
            var medias = _unitOfWork.MediaRepo.GetByCategoryId(id);
            var mappedMedias = _mapper.Map<List<MediaViewModel>>(medias);
            return View(mappedMedias);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCategoryViewModel category)
        {
            try
            {
                var mappedCategory = _mapper.Map<Category>(category);
                _unitOfWork.CategoryRepo.Insert(mappedCategory);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var category = _unitOfWork.CategoryRepo.GetById(id);
            var mappedCategory = _mapper.Map<EditCategoryViewModel>(category);
            return View(mappedCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCategoryViewModel viewModel)
        {
            try
            {
                var category = _mapper.Map<Category>(viewModel);
                _unitOfWork.CategoryRepo.Update(category);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var category = _unitOfWork.CategoryRepo.GetById(id);
            var mappedCategory = _mapper.Map<CategoryViewModel>(category);
            return View(mappedCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CategoryViewModel category)
        {
            try
            {
                _unitOfWork.CategoryRepo.Delete(id);
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
