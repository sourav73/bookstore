using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookstore.Dtos.Category;
using bookstore.Models;

namespace bookstore.Services.Category
{
    public interface ICategoryService
    {
        Task<ServiceResponse<GetCategoryDto>> AddCategory(AddCategoryDto category);
        Task<ServiceResponse<GetCategoryDto>> UpdateCategory(UpdateCategoryDto category);
        Task<ServiceResponse<GetCategoryDto>> DeleteCategory(int id);
        Task<ServiceResponse<List<bookstore.Models.Category>>> GetCategories();
        Task<ServiceResponse<GetCategoryDto>> GetCategoryById(int id);
    }
}