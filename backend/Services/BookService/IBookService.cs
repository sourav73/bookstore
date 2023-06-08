using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookstore.Dtos.Book;
using bookstore.Models;

namespace bookstore.Services.BookService
{
    public interface IBookService
    {
        Task<ServiceResponse<GetBookDto>> AddBook(AddBookDto book);
        Task<ServiceResponse<GetBookDto>> UpdateBook(UpdateBookDto book);
        Task<ServiceResponse<GetBookDto>> DeleteBook(int id);
        Task<ServiceResponse<List<GetBookDto>>> GetBooks();
        Task<ServiceResponse<GetBookDto>> GetBookById(int id);
    }
}