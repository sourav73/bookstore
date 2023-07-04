using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookstore.Data;
using bookstore.Dtos.Author;
using bookstore.Models;
using bookstore.Services.Author;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bookstore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAuthorService _authorService;
        public AuthorController(DataContext context, IAuthorService authorService)
        {
            _authorService = authorService;
            _context = context;
        }

        // Creating an author
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetAuthorDto>>> AddAuthor(AddAuthorDto author)
        {
            return Ok(await _authorService.AddAuthor(author));
        }
        // Getting all authors
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Author>>>> GetAllAuthors()
        {
            return Ok(await _authorService.GetAuthors());
        }
        // Get specific author
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetAuthorDto>>> GetAuthorById(int id)
        {
            var response = await _authorService.GetAuthorById(id);
            if (response.Data == null)
                return NotFound(response);
            return Ok(response);
        }
        // update an author
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetAuthorDto>>> UpdateAuthor(int id, AddAuthorDto author)
        {
            var response = await _authorService.UpdateAuthor(id, author);
            if (response.Data == null)
                return BadRequest(response);
            return Ok(response);
        }
        // delete an author
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetAuthorDto>>> DeleteAuthor(int id)
        {
            var response = await _authorService.DeleteAuthor(id);
            if (response.Data == null)
                return NotFound(response);
            return Ok(response);
        }
    }
}