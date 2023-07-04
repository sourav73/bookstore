using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using bookstore.Data;
using bookstore.Dtos.Auth;
using bookstore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace bookstore.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public AuthService(DataContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<ServiceResponse<AuthOutputDto>> Login(string username, string password)
        {
            var response = new ServiceResponse<AuthOutputDto>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash!, user.PasswordSalt!))
            {
                response.Success = false;
                response.Message = "Wrong password";
            }
            else
            {
                var existingRefreshToken = await _context.RefreshTokens.Where(t => t.FkUserId == user.UserId).ToListAsync();
                _context.RefreshTokens.RemoveRange(existingRefreshToken);
                response.Data = new AuthOutputDto()
                {
                    Token = CreateToken(user),
                    RefreshToken = await CreateRefreshToken(user.UserId),
                    UserId = user.UserId
                };
            }
            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var response = new ServiceResponse<int>();
            if (await UserExist(user.Username))
            {
                response.Success = false;
                response.Message = "User already exist";
                return response;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            response.Data = user.UserId;
            return response;
        }

        public async Task<bool> UserExist(string username)
        {
            if (await _context.Users.AnyAsync(u => u.Username == username))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        public async Task<ServiceResponse<AuthOutputDto>> RefreshToken(string token)
        {
            var response = new ServiceResponse<AuthOutputDto>();
            /*
             Get Refresh token from repository to generate a new access token
             */
            RefreshToken currentToken = await GetRefreshToken(token);
            if (currentToken == null)
                throw new Exception("Invalid token request!");

            // User userInfo = GetUserInfoById(currentToken.FkUserId);
            User userInfo = _context.Users.Where(u => u.UserId == currentToken.FkUserId).FirstOrDefault();
            if (userInfo == null)
                throw new Exception("Invalid token request!");
            /*
             Generate new access token using refresh token
             */
            var newToken = CreateToken(userInfo);


            var refreshToken = await CreateRefreshToken(userInfo.UserId);
            DeleteRefreshToken(currentToken);
            response.Success = true;
            response.Message = "New tokens generated";
            response.Data = new AuthOutputDto() { Token = newToken, RefreshToken = refreshToken, UserId = userInfo.UserId };
            return response;
        }

        private async Task<string> CreateRefreshToken(int userId)
        {
            RefreshToken Token = new RefreshToken();
            Token.FkUserId = userId;
            Token.Token = Guid.NewGuid().ToString();
            Token.ExipiresAt = DateTime.Now.AddDays(1);
            _context.RefreshTokens.Add(Token);
            await _context.SaveChangesAsync();
            return Token.Token;
        }

        public async Task<ServiceResponse<RefreshToken>> GetRefreshTokenByToken(string token)
        {
            var response = new ServiceResponse<RefreshToken>();
            var rToken = await _context.RefreshTokens.Where(t => t.Token == token && t.ExipiresAt > DateTime.Now).FirstOrDefaultAsync();
            response.Success = rToken == null ? false : true;
            response.Message = rToken == null ? "Token not found" : "Token found";
            response.Data = rToken;
            return response;
        }
        private async Task<RefreshToken> GetRefreshToken(string token)
        {
            return await _context.RefreshTokens.Where(t => t.Token == token && t.ExipiresAt > DateTime.Now).FirstOrDefaultAsync();
        }

        public void DeleteRefreshToken(RefreshToken token)
        {
            _context.RefreshTokens.Remove(token);
            _context.SaveChanges();
        }
        public async Task<int> DeleteExpiredRefreshTokens()
        {
            var expiredTokens = _context.RefreshTokens.Where(t => t.ExipiresAt <= DateTime.Now).ToList();
            _context.RefreshTokens.RemoveRange(expiredTokens);
            // await _context.SaveChangesAsync();
            return await _context.SaveChangesAsync();
        }
    }
}