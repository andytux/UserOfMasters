using UserOfMasters.Data;
using UserOfMasters.Data.Models;

namespace UserOfMasters.Services
{
    public class UserService
    {
        private readonly AppDbContext dbContext;

        public UserService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public User GetUserWithUserName(string userName)
        {
            return dbContext.Users.FirstOrDefault(u=>u.UserName == userName);
        }

        public User GetUserWithUseId(Guid userId)
        {
            return dbContext.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public bool UserAlreadyExists (string userName)
        {
            return dbContext.Users.Any(u => u.UserName == userName);
        }

        public async Task AddUserAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
        }

        public List<User> GetUsers()
        {
            return dbContext.Users.ToList();
        }
    }
}
