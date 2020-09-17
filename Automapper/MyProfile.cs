using AutoMapper;
using Image_Gallery.Models;
using Image_Gallery.ViewModels.CategoryViewModels;
using Image_Gallery.ViewModels.MediaViewModels;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Image_Gallery.Automapper
{
    public class MyProfile : Profile
    {
        public MyProfile()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Category, EditCategoryViewModel>().ReverseMap();
            CreateMap<Category, CreateCategoryViewModel>().ReverseMap();

            CreateMap<Media, MediaEditViewModel>().ReverseMap();

            CreateMap<Media, MediaViewModel>().ForMember(
                dest => dest.CategoryTitle,
                opt => opt.MapFrom(
                    src => src.Category.Title));

            CreateMap<Media, CategoryViewModel>().ForPath(
                dest => dest.CategoryMedia,
                opt => opt.MapFrom(
                    src => src)).ReverseMap();

            CreateMap<Media, Media>().ForMember(
                dest => dest.Category,
                opt => opt.MapFrom(
                    src => src.Category));

        }
    }
    
}
