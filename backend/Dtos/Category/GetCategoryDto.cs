using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookstore.Dtos.Category
{
    public class GetCategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = "";
    }
}