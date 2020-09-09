using Restaurant.Database.Models;
using Restaurant.Services.Models.User;
using System.Collections.Generic;

namespace Restaurant.Services
{
    public interface IUserService
    {
        UserDto CreateUser(string name, string username, string password, List<Role> roles);

        void UpdateUser(User user, List<UserRole> userRoles);

        void DeleteUser(User user);

        IEnumerable<UserDto> GetAllUsers();
    }
}
