using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bookstore.Dtos.Book
{
    public class AddBookDto
    {
        public string Name { get; set; } = "";
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
    }
}