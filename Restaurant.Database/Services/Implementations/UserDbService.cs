using System.Linq;
using Restaurant.Database.Data;
using Restaurant.Database.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

        public User CreateUser(string name, string username, string password, List<Role> roles)
        {
            User user = new User()
            {
                Name = name,
                Username = username,
                Password = password
            };

            context.Users.Add(user);

            List<UserRole> userRoles = roles.Select(r => new UserRole()
            {
                User = user,
                UserId = user.Id,
                RoleId = r.Id,
                Role = r
            }).ToList();

            foreach (UserRole userRole in userRoles)
            {
                user.Roles.Add(userRole);
            }

            context.SaveChanges();

            return user;
        }

        public void UpdateUser(User user, List<UserRole> userRoles)
        {
            User entity = context.Users.Include(u => u.Roles).FirstOrDefault(u => u.Id == user.Id);

            if (entity == null)
                return;

            entity.Name = user.Name;
            entity.Username = user.Username;
            entity.Password = user.Password;

            List<UserRole> rolesForRemoval = new List<UserRole>();
            foreach (UserRole role in entity.Roles)
            {
                if (userRoles.FirstOrDefault(ur => ur.RoleId == role.RoleId && ur.UserId == role.UserId) == null)
                {
                    rolesForRemoval.Add(role);
                }
            }

            foreach (UserRole role in rolesForRemoval)
            {
                entity.Roles.Remove(role);
            }

            foreach (UserRole userRole in userRoles)
            {
                if (entity.Roles.FirstOrDefault(r => r.RoleId == userRole.RoleId && r.UserId == userRole.UserId) == null)
                {
                    entity.Roles.Add(userRole);
                }
            }

            context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            context.Users.Remove(user);

            context.SaveChanges();
        }

        public IQueryable<User> GetAllUsers()
        {
            return context.Users
                          .Include(user => user.Roles)
                          .ThenInclude(ur => ur.Role);
        }

        #endregion
    }
}
