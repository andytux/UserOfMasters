using System.Security.Cryptography;
using System.Text;
using UserOfMasters.Data;
using UserOfMasters.Data.Models;

namespace UserOfMasters.Services
{
    public class AuthService
    {
        private readonly AppDbContext dbContext;

        public UserService UserService { get; }

        public AuthService(AppDbContext dbContext, UserService userService)
        {
            this.dbContext = dbContext;
            UserService = userService;
        }

        public string GetSaltOfGuid(Guid userId)
        {
            return userId.ToString().Substring(0, 8);
        }

        private byte[] GetCombinedBytes (string password, string salt)
        {
            var combined = password+salt;

            return Encoding.UTF8.GetBytes(combined);
        }

        public string HashPassword(string password, string salt)
        {
            var combinedBytes = GetCombinedBytes(password, salt);

            using(var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(combinedBytes);

                return Convert.ToBase64String(hashBytes);
            }
        }

        public bool VerifyPassword (string password, string storedHash, string salt)
        {
            var hashOfInput = HashPassword(password, salt);

            if (hashOfInput != storedHash)
            {
                return false;
            }

            return true;
        }

        public async Task<string> GetResetToken(User user)
        {
            user.ResetToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            user.TokenExpire = DateTime.UtcNow.AddMinutes(10);

            await dbContext.SaveChangesAsync();

            return user.ResetToken;
        }
    }
}
