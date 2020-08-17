using System.Linq;
using Microsoft.EntityFrameworkCore;
using Restaurant.Database.Data;
using Restaurant.Database.Models;

namespace Restaurant.Database.Services.Implementations
{
    public class UserDbService : IUserDbService
    {
        #region Declarations

        private readonly RestaurantDbContext context;

        #endregion

        #region Constructors

        public UserDbService()
        {
            context = new RestaurantDbContext();
        }

        #endregion

        #region Methods

        public void CreateUser(string name, string username, string password, int roleId)
        {
            User user = new User()
            {
                Name = name,
                Username = username,
                Password = password
            };

            UserRole userRole = new UserRole()
            {
                User = user,
                RoleId = roleId
            };

            user.Roles.Add(userRole);

            context.Users.Add(user);

            context.SaveChanges();
        }

        public IQueryable<User> GetAllUsers()
        {
            return context.Users
                          .Include(user => user.Roles)
                          .ThenInclude(user => user.Role);
        }

        #endregion
    }
}
