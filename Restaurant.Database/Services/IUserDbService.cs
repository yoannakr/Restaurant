using Restaurant.Database.Models;

namespace Restaurant.Database.Services
{
    public interface IUserDbService
    {
        void CreateUser(string name, string username, string password, Role role);
    }
}
