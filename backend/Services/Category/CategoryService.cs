using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bookstore.Data;
using bookstore.Dtos.Category;
using bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoryService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetCategoryDto>> AddCategory(AddCategoryDto newCategory)
        {
            var response = new ServiceResponse<GetCategoryDto>();
            var category = _mapper.Map<bookstore.Models.Category>(newCategory);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetCategoryDto>(category);
            return response;
        }

        public async Task<ServiceResponse<GetCategoryDto>> DeleteCategory(int id)
        {
            var response = new ServiceResponse<GetCategoryDto>();
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                response.Success = false;
                response.Message = "Category not found!";
            }
            else
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCategoryDto>(category);
            }
            return response;
        }

        public async Task<ServiceResponse<List<bookstore.Models.Category>>> GetCategories()
        {
            var response = new ServiceResponse<List<bookstore.Models.Category>>();
            response.Data = await _context.Categories.Select(b => b).ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<GetCategoryDto>> GetCategoryById(int id)
        {
            var response = new ServiceResponse<GetCategoryDto>();
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if (category != null)
            {
                response.Data = _mapper.Map<GetCategoryDto>(category);
            }
            else
            {
                response.Success = false;
                response.Message = "Category not found!";
            }
            return response;
        }

        public async Task<ServiceResponse<GetCategoryDto>> UpdateCategory(UpdateCategoryDto category)
        {
            var response = new ServiceResponse<GetCategoryDto>();
            var targetCategory = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == category.Id);
            if (targetCategory != null)
            {
                targetCategory.Name = category.Name;
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCategoryDto>(targetCategory);
            }
            else
            {
                response.Success = false;
                response.Message = "Category not found!";
            }
            return response;
        }
    }
}