using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookstore.Dtos.Auth;
using bookstore.Models;

namespace bookstore.Services.Authentication
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<AuthOutputDto>> Login(string username, string password);
        Task<bool> UserExist(string username);
        Task<ServiceResponse<AuthOutputDto>> RefreshToken(string token);
        Task<ServiceResponse<RefreshToken>> GetRefreshTokenByToken(string token);
    }
}