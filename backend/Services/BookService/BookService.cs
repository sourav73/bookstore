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
            // if (await BookExist(id))
            if (await BookExist(id) != null)
            {
                var book = _context.Books.Include(b => b.Author).Include(b => b.Category).First(b => b.BookId == id);
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
            var existingBook = await BookExist(book.BookId);
            // if (await BookExist(book.BookId))
            if (existingBook != null)
            {
                // var targetBook = _context.Books.First(b => b.BookId == book.BookId);
                // targetBook.Name = book.Name;
                // targetBook.Author = _context.Authors.First(a => a.AuthorId == book.AuthorId);
                // targetBook.Category = _context.Categories.First(c => c.CategoryId == book.CategoryId);
                // response.Data = _mapper.Map<GetBookDto>(targetBook);
                if (await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == book.CategoryId) == null)
                {
                    response.Success = false;
                    response.Message = "Category not found!";
                    return response;
                }
                if (await _context.Authors.FirstOrDefaultAsync(a => a.AuthorId == book.AuthorId) == null)
                {
                    response.Success = false;
                    response.Message = "Author not found!";
                    return response;
                }

                existingBook.Name = book.Name;
                existingBook.Author = _context.Authors.First(a => a.AuthorId == book.AuthorId);
                existingBook.Category = _context.Categories.First(c => c.CategoryId == book.CategoryId);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetBookDto>(existingBook);
            }
            else
            {
                response.Success = false;
                response.Message = "Book not found!";
            }
            return response;
        }

        // private async Task<bool> BookExist(int id)
        // {
        //     var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);
        //     if (book == null)
        //         return false;
        //     else
        //         return true;
        // }

        private async Task<Book?> BookExist(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);
            if (book == null)
                return null;
            else
                return book;
        }
    }
}