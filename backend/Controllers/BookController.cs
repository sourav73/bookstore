using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookstore.Data;
using bookstore.Dtos.Book;
using bookstore.Models;
using bookstore.Services.BookService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IBookService _bookService;
        public BookController(DataContext context, IBookService bookService)
        {
            _bookService = bookService;
            _context = context;
        }

        // Creating a book
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetBookDto>>> AddBook(AddBookDto book)
        {
            return Ok(await _bookService.AddBook(book));
        }
        // Getting all books
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetBookDto>>>> GetAllBooks()
        {
            return Ok(await _bookService.GetBooks());
        }
        // Get specific book
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetBookDto>>> GetBookById(int id)
        {
            var response = await _bookService.GetBookById(id);
            if (response.Data == null)
                return NotFound(response);
            return Ok(response);
        }
        // update a book
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetBookDto>>> UpdateBook(UpdateBookDto book)
        {
            var response = await _bookService.UpdateBook(book);
            if (response.Data == null)
                return BadRequest(response);
            return Ok(response);
        }
        // delete a book
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetBookDto>>> DeleteBook(int id)
        {
            var response = await _bookService.DeleteBook(id);
            if (response.Data == null)
                return NotFound(response);
            return Ok(response);
        }
    }
}