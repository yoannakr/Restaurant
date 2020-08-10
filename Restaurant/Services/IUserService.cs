using Restaurant.Database.Models;

namespace Restaurant.Services
{
    public interface IUserService
    {
        void CreateUser(string name, string username, string password, Role role);
    }
}
