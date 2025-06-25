using System.Security.Cryptography;
using System.Text;
using BasicApi.Models;

namespace BasicApi.Services
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string username, string password);
        Task<User?> RegisterAsync(RegisterDto registerDto);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByIdAsync(int id);
        Task<bool> UserExistsAsync(string username, string email);
    }

    public class UserService : IUserService
    {
        // In-memory storage for demo purposes
        // In a real application, you would use a database
        private static List<User> _users = new List<User>
        {
            new User 
            { 
                Id = 1, 
                Username = "admin", 
                Email = "admin@basicapi.com", 
                PasswordHash = HashPassword("admin123"), 
                Role = "Admin" 
            },
            new User 
            { 
                Id = 2, 
                Username = "user", 
                Email = "user@basicapi.com", 
                PasswordHash = HashPassword("user123"), 
                Role = "User" 
            }
        };
        private static int _nextId = 3;

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            await Task.Delay(1); // Simulate async operation
            
            var user = _users.FirstOrDefault(u => 
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && 
                u.IsActive);

            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                return null;
            }

            return user;
        }

        public async Task<User?> RegisterAsync(RegisterDto registerDto)
        {
            await Task.Delay(1); // Simulate async operation

            if (await UserExistsAsync(registerDto.Username, registerDto.Email))
            {
                return null;
            }

            var user = new User
            {
                Id = _nextId++,
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = HashPassword(registerDto.Password),
                Role = "User",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _users.Add(user);
            return user;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            await Task.Delay(1); // Simulate async operation
            return _users.FirstOrDefault(u => 
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && 
                u.IsActive);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            await Task.Delay(1); // Simulate async operation
            return _users.FirstOrDefault(u => u.Id == id && u.IsActive);
        }

        public async Task<bool> UserExistsAsync(string username, string email)
        {
            await Task.Delay(1); // Simulate async operation
            return _users.Any(u => 
                (u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) || 
                 u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)) && 
                u.IsActive);
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + "BasicApiSalt"));
            return Convert.ToBase64String(hashedBytes);
        }

        private static bool VerifyPassword(string password, string hash)
        {
            var hashedPassword = HashPassword(password);
            return hashedPassword == hash;
        }
    }
}
