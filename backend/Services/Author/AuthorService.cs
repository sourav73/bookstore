using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bookstore.Data;
using bookstore.Dtos.Author;
using bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Services.Author
{
    public class AuthorService : IAuthorService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AuthorService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetAuthorDto>> AddAuthor(AddAuthorDto author)
        {
            var response = new ServiceResponse<GetAuthorDto>();
            var authorToAdd = _mapper.Map<bookstore.Models.Author>(author);
            _context.Authors.Add(authorToAdd);
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetAuthorDto>(authorToAdd);
            return response;
        }

        public async Task<ServiceResponse<GetAuthorDto>> DeleteAuthor(int id)
        {
            var response = new ServiceResponse<GetAuthorDto>();
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.AuthorId == id);
            if (author == null)
            {
                response.Success = false;
                response.Message = "Author not found!";
            }
            else
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetAuthorDto>(author);
            }
            return response;
        }

        public async Task<ServiceResponse<GetAuthorDto>> GetAuthorById(int id)
        {
            var response = new ServiceResponse<GetAuthorDto>();
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.AuthorId == id);
            if (author == null)
            {
                response.Success = false;
                response.Message = "Author not found!";
            }
            else
            {
                response.Data = _mapper.Map<GetAuthorDto>(author);
            }
            return response;
        }

        public async Task<ServiceResponse<List<Models.Author>>> GetAuthors()
        {
            var response = new ServiceResponse<List<Models.Author>>();
            response.Data = await _context.Authors.Select(b => b).ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<GetAuthorDto>> UpdateAuthor(int id, AddAuthorDto newAuthor)
        {
            var response = new ServiceResponse<GetAuthorDto>();
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.AuthorId == id);
            if (author == null)
            {
                response.Success = false;
                response.Message = "Author not found!";
            }
            else
            {
                author.Name = newAuthor.Name;
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetAuthorDto>(author);
            }
            return response;
        }
    }
}