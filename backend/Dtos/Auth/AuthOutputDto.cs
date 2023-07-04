using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookstore.Dtos.Auth
{
    public class AuthOutputDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
    }
}