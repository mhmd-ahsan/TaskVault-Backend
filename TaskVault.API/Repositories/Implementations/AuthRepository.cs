using Microsoft.EntityFrameworkCore;
using NotificationApp_New.Services;
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
        private readonly INotificationService _service;
        public AuthRepository(AppDbContext context, JwtHelper jwtHelper, INotificationService service)
        {
            _context = context;
            _jwtHelper = jwtHelper;
            _service = service;
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
            var user = await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return new HelperResponse
                {
                    Success = false,
                    Message = "Invalid Credentials"
                };

            // Generate JWT token
            var token = _jwtHelper.GenerateJwt(user.Id, user.EmailAddress);

            // Create notification object
            var notification = new Notification
            {
                Title = "Welcome!",
                Message = $"Hello {user.Name}, welcome back!",
                SentTo = user.Id.ToString(),
                SentBy = "System",
                CreatedAt = DateTime.UtcNow
            };

            // Save to database
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            // ✅ Send notification via SignalR after 10 seconds
            _ = Task.Run(async () =>
            {
                await Task.Delay(5000); // 10 seconds
                await _service.SendAsync(notification, user.Id.ToString());
            });

            return new HelperResponse
            {
                Success = true,
                Message = "Login successful",
                Data = token
            };
        }
    }
}
