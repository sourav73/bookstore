using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookstore.Dtos.Book
{
    public class UpdateBookDto
    {
        public int BookId { get; set; }
        public string Name { get; set; } = "";
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
    }
}