namespace Restaurant.Services
{
    public interface IUserService
    {
        void CreateUser(string name, string username, string password, int roleId);
    }
}
