using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bookstore.Data;
using bookstore.Dtos.Book;
using bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BookService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<GetBookDto>> AddBook(AddBookDto book)
        {
            var response = new ServiceResponse<GetBookDto>();
            var bookToAdd = _mapper.Map<Book>(book);
            bookToAdd.Author = await _context.Authors.FirstAsync(a => a.AuthorId == book.AuthorId);
            bookToAdd.Category = await _context.Categories.FirstAsync(c => c.CategoryId == book.CategoryId);
            _context.Books.Add(bookToAdd);
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetBookDto>(bookToAdd);
            return response;
        }

        public async Task<ServiceResponse<GetBookDto>> DeleteBook(int id)
        {
            var response = new ServiceResponse<GetBookDto>();
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);
            if (book != null)
            {
                response.Data = _mapper.Map<GetBookDto>(book);
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            else
            {
                response.Success = false;
                response.Message = "Book not found!";
            }
            return response;
        }

        public async Task<ServiceResponse<GetBookDto>> GetBookById(int id)
        {
            var response = new ServiceResponse<GetBookDto>();
            if (await BookExist(id))
            {
                var book = await _context.Books.Include(b => b.Author).Include(b => b.Category).FirstAsync(b => b.BookId == id);
                response.Data = _mapper.Map<GetBookDto>(book);
            }
            else
            {
                response.Success = false;
                response.Message = "Book not found!";
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetBookDto>>> GetBooks()
        {
            var response = new ServiceResponse<List<GetBookDto>>();
            var books = await _context.Books.Include(b => b.Author).Include(b => b.Category).ToListAsync();
            // response.Data = books.Select(b => _mapper.Map<GetBookDto>(b)).ToList();
            response.Data = books.Select(b => _mapper.Map<GetBookDto>(b)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetBookDto>> UpdateBook(UpdateBookDto book)
        {
            var response = new ServiceResponse<GetBookDto>();
            if (await BookExist(book.BookId))
            {
                var targetBook = await _context.Books.FirstAsync(b => b.Name.ToLower() == book.Name.ToLower());
                targetBook.Name = book.Name;
                targetBook.Author = _context.Authors.First(a => a.AuthorId == book.AuthorId);
                targetBook.Category = _context.Categories.First(c => c.CategoryId == book.CategoryId);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetBookDto>(targetBook);
                // response.Data = new GetBookDto
                // {
                //     Id = book.Id,
                //     Name = book.Name,
                //     Author = book.Author,
                //     Category = book.Category
                // };
            }
            else
            {
                response.Success = false;
                response.Message = "Book not found!";
            }
            return response;
        }

        private async Task<bool> BookExist(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);
            if (book == null)
                return false;
            else
                return true;
        }
    }
}