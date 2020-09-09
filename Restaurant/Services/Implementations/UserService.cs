using Restaurant.Common.Helpers;
using Restaurant.Database.Models;
using System.Collections.Generic;
using Restaurant.Database.Services;
using Restaurant.Database.Services.Implementations;
using Restaurant.Services.Models.User;
using System.Linq;

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

        public UserDto CreateUser(string name, string username, string password, List<Role> roles)
        {
            string securityPassword = HashingPasswordHelper.ComputePasswordHashing(password);

            User user = userDb.CreateUser(name, username, securityPassword, roles);

            UserDto userDto = new UserDto()
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Password = user.Password,
                Roles = user.Roles,
                Payments = user.Payments
            };

            return userDto;
        }

        public void UpdateUser(User user, List<UserRole> userRoles)
        {
            userDb.UpdateUser(user, userRoles);
        }

        public void DeleteUser(User user)
        {
            userDb.DeleteUser(user);
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            return userDb.GetAllUsers().Select(u=>new UserDto()
            {
                Id = u.Id,
                Name = u.Name,
                Username = u.Username,
                Password = u.Password,
                Roles = u.Roles,
                Payments = u.Payments
            });
        }

        #endregion
    }
}
