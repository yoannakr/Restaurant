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

        public void CreateUser(string name, string username, string password, int roleId)
        {
            userDb.CreateUser(name, username, password, roleId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return userDb.GetAllUsers();
        }

        #endregion
    }
}
