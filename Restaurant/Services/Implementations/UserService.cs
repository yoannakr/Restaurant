using Restaurant.Common.Helpers;
using Restaurant.Database.Models;
using System.Collections.Generic;
using Restaurant.Database.Services;
using Restaurant.Database.Services.Implementations;

namespace Restaurant.Services.Implementations
{
    public class UserService : IUserService
    {
        #region Declarations

        private readonly IUserDbService userDb;

        #endregion

        #region Constructors

        public UserService()
        {
            userDb = new UserDbService();
        }

        #endregion

        #region Methods

        public User CreateUser(string name, string username, string password, List<Role> roles)
        {
            string securityPassword = HashingPasswordHelper.ComputePasswordHashing(password);

            User user = userDb.CreateUser(name, username, securityPassword, roles);

            return user;
        }

        public void UpdateUser(User user, List<UserRole> userRoles)
        {
            userDb.UpdateUser(user, userRoles);
        }

        public void DeleteUser(User user)
        {
            userDb.DeleteUser(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return userDb.GetAllUsers();
        }

        #endregion
    }
}
