using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookstore.Dtos.Author;
using bookstore.Dtos.Category;
using bookstore.Models;

namespace bookstore.Dtos.Book
{
    public class GetBookDto
    {
        public int BookId { get; set; }
        public string Name { get; set; } = "";
        // public int CategoryId { get; set; }
        // public int AuthorId { get; set; }
        public GetAuthorDto Author { get; set; } = null!;
        public GetCategoryDto Category { get; set; } = null!;
    }
}