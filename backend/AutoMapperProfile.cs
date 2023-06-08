using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bookstore.Dtos.Author;
using bookstore.Dtos.Book;
using bookstore.Dtos.Category;
using bookstore.Models;

namespace bookstore
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddCategoryDto, Category>();
            CreateMap<Category, GetCategoryDto>();
            CreateMap<AddBookDto, Book>();
            CreateMap<Book, GetBookDto>();
            CreateMap<AddAuthorDto, Author>();
            CreateMap<Author, GetAuthorDto>();
        }
    }
}