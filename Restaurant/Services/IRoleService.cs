using System.Collections.Generic;
using Restaurant.Services.Models.Role;

namespace Restaurant.Services
{
    public interface IRoleService
    {
        void CreateRole(string name);

        IEnumerable<RoleDto> GetAllRoles();
    }
}
