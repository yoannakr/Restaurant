using System.Collections.Generic;
using Restaurant.Database.Models;
using Restaurant.Services.Models.Role;

namespace Restaurant.Services
{
    public interface IRoleService
    {
        RoleDto CreateRole(string name);

        IEnumerable<RoleDto> GetAllRoles();
    }
}
