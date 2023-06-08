using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookstore.Dtos.Author
{
    public class GetAuthorDto
    {
        public int AuthorId { get; set; }
        public string Name { get; set; } = "";
    }
}