using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookstore.Data;
using bookstore.Dtos.Category;
using bookstore.Models;
using bookstore.Services.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bookstore.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ICategoryService _categoryService;
        public CategoryController(DataContext context, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _context = context;
        }

        // Creating a category
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCategoryDto>>> AddCategory(AddCategoryDto category)
        {
            return Ok(await _categoryService.AddCategory(category));
        }
        // Getting all category
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Category>>>> GetAllCategories()
        {
            return Ok(await _categoryService.GetCategories());
        }
        // Get specific category
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCategoryDto>>> GetCategoryById(int id)
        {
            var response = await _categoryService.GetCategoryById(id);
            if (response.Data == null)
                return NotFound(response);
            return Ok(response);
        }
        // update a category
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCategoryDto>>> UpdateCategory(UpdateCategoryDto category)
        {
            var response = await _categoryService.UpdateCategory(category);
            if (response.Data == null)
                return BadRequest(response);
            return Ok(response);
        }
        // delete a category
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCategoryDto>>> DeleteCategory(int id)
        {
            var response = await _categoryService.DeleteCategory(id);
            if (response.Data == null)
                return NotFound(response);
            return Ok(response);
        }
    }
}