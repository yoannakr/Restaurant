﻿using Restaurant.Database.Data;
using Restaurant.Database.Models;
using System.Collections.Generic;

namespace Restaurant.Database.Services.Implementations
{
    public class UserDbService : IUserDbService
    {
        #region Declarations

        private readonly RestaurantDbContext context;

        #endregion

        #region Constructor

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

        #endregion
    }
}