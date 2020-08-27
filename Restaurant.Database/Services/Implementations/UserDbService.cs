using System.Collections.Generic;
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

        public void CreateUser(string name, string username, string password, List<int> rolesId)
        {
            User user = new User()
            {
                Name = name,
                Username = username,
                Password = password
            };

            ICollection<UserRole> userRoles = rolesId.Select(id => new UserRole()
            {
                User = user,
                RoleId = id
            }).ToList();

            foreach (UserRole userRole in userRoles)
            {
                user.Roles.Add(userRole);
            }

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
