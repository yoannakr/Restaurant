using Restaurant.Database.Models;
using System.Collections.Generic;

namespace Restaurant.Services
{
    public interface IUserService
    {
        User CreateUser(string name, string username, string password, List<Role> roles);

        void UpdateUser(User user, List<UserRole> userRoles);

        void DeleteUser(User user);

        IEnumerable<User> GetAllUsers();
    }
}
