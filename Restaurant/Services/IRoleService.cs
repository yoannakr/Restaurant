using System.Collections.Generic;
using Restaurant.Database.Models;

namespace Restaurant.Services
{
    public interface IRoleService
    {
        void CreateRole(string name);

        IEnumerable<Role> GetAllRoles();
    }
}
