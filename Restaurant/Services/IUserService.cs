using Restaurant.Database.Models;
using System.Collections.Generic;

namespace Restaurant.Services
{
    public interface IUserService
    {
        void CreateUser(string name, string username, string password, List<int> rolesId);

        IEnumerable<User> GetAllUsers();
    }
}
