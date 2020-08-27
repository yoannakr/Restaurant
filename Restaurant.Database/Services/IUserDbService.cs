using System.Linq;
using System.Collections.Generic;
using Restaurant.Database.Models;

namespace Restaurant.Database.Services
{
    public interface IUserDbService
    {
        void CreateUser(string name, string username, string password, List<int> rolesId);

        IQueryable<User> GetAllUsers();
    }
}
