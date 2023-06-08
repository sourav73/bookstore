using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookstore.Dtos.Author;
using bookstore.Models;

namespace bookstore.Services.Author
{
    public interface IAuthorService
    {
        Task<ServiceResponse<GetAuthorDto>> AddAuthor(AddAuthorDto book);
        Task<ServiceResponse<GetAuthorDto>> UpdateAuthor(int id, AddAuthorDto author);
        Task<ServiceResponse<GetAuthorDto>> DeleteAuthor(int id);
        Task<ServiceResponse<List<bookstore.Models.Author>>> GetAuthors();
        Task<ServiceResponse<GetAuthorDto>> GetAuthorById(int id);
    }
}