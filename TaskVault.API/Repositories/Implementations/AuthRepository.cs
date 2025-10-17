using Microsoft.EntityFrameworkCore;
using TaskVault.API.Data;
using TaskVault.API.Dtos.AuthDtos;
using TaskVault.API.Helpers;
using TaskVault.API.Models;
using TaskVault.API.Repositories.Interfaces;

namespace TaskVault.API.Repositories.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly JwtHelper _jwtHelper;
        public AuthRepository(AppDbContext context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        public async Task<HelperResponse> RegisterAsync(RegisterDto dto)
        {
            //Check if email already exists
            if (await _context.Users.AnyAsync(u => u.EmailAddress == dto.Email))
                return new HelperResponse
                {
                    Success = false,
                    Message = "Email already exists"
                };
            var user = new User
            {
                Name = dto.Name,
                EmailAddress = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new HelperResponse
            {
                Success = true,
                Message = "User registered successfully"
            };
        }

        public async Task<HelperResponse> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync( u => u.EmailAddress == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return new HelperResponse
                {
                    Success = false,
                    Message = "Invalid Credentials"
                };
            // Generate token 
            var token = _jwtHelper.GenerateJwt(user.Id, user.EmailAddress);

            return new HelperResponse
            {
                Success = true,
                Message = " Login successfull",
                Data = token
            };
        }
    }
}
