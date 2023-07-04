using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bookstore.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExipiresAt { get; set; }
        public int FkUserId { get; set; }
        [ForeignKey("FkUserId")]
        public User User { get; set; }
    }
}