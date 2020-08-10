using Restaurant.Database.Models;
using Restaurant.Database.Services;
using Restaurant.Database.Services.Implementations;

namespace Restaurant.Services.Implementations
{
    public class UserService : IUserService
    {
        #region Declarations

        private readonly IUserDbService userDb;

        #endregion

        #region Constructor

        public UserService()
        {
            userDb = new UserDbService();
        }

        #endregion

        #region Methods

        public void CreateUser(string name, string username, string password, Role role)
        {
            userDb.CreateUser(name, username, password, role);
        }

        #endregion
    }
}
