using System.Linq;
using Restaurant.Database.Data;
using Restaurant.Database.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;

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
            User entityUser = context.Users.Include(u => u.Roles).FirstOrDefault(u => u.Id == user.Id);

            if (entityUser == null)
                throw new Exception();

            entityUser.Name = user.Name;
            entityUser.Username = user.Username;
            entityUser.Password = user.Password;

            List<UserRole> rolesForRemoval = new List<UserRole>();
            foreach (UserRole role in entityUser.Roles)
            {
                if (userRoles.FirstOrDefault(ur => ur.RoleId == role.RoleId && ur.UserId == role.UserId) == null)
                {
                    rolesForRemoval.Add(role);
                }
            }

            foreach (UserRole role in rolesForRemoval)
            {
                entityUser.Roles.Remove(role);
            }

            foreach (UserRole userRole in userRoles)
            {
                if (entityUser.Roles.FirstOrDefault(r => r.RoleId == userRole.RoleId && r.UserId == userRole.UserId) == null)
                {
                    entityUser.Roles.Add(userRole);
                }
            }

            context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            User entityUser = context.Users.FirstOrDefault(u => u.Id == user.Id);

            if (entityUser == null)
                throw new Exception();

            context.Users.Remove(entityUser);

            context.SaveChanges();
        }

        public IQueryable<User> GetAllUsers()
        {
            return context.Users
                          .Include(user => user.Roles)
                          .ThenInclude(ur => ur.Role)
                          .Include(user=>user.Payments);
        }

        #endregion
    }
}
