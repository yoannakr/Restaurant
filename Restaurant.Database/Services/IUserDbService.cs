using System.Linq;
using System.Collections.Generic;
using Restaurant.Database.Models;

namespace Restaurant.Database.Services
{
    public interface IUserDbService
    {
        User CreateUser(string name, string username, string password, List<Role> roles);

        void UpdateUser(User user, List<UserRole> userRoles);

        void DeleteUser(User user);

        IQueryable<User> GetAllUsers();
    }
}
