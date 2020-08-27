using System.Linq;
using System.Collections.Generic;
using Restaurant.Database.Services;
using Restaurant.Services.Models.Role;
using Restaurant.Database.Services.Implementations;

namespace Restaurant.Services.Implementations
{
    public class RoleService : IRoleService
    {
        #region Declarations

        private readonly IRoleDbService roleDb;

        #endregion

        #region Constructors

        public RoleService()
        {
            roleDb = new RoleDbService();
        }

        #endregion

        #region Methods


        public void CreateRole(string name)
        {
            roleDb.CreateRole(name);
        }


        public IEnumerable<RoleDto> GetAllRoles()
        {
            return roleDb.GetAllRoles().Select(r => new RoleDto()
            {
                Id = r.Id,
                Name = r.Name
            });
        }

        #endregion
    }
}
