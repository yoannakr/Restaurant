using System.Linq;
using Restaurant.Database.Models;

namespace Restaurant.Database.Services
{
    public interface IRoleDbService
    {
        void CreateRole(string name);

        IQueryable<Role> GetAllRoles();

    }
}
