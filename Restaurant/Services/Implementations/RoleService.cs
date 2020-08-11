using System.Linq;
using Restaurant.Database.Models;
using System.Collections.Generic;
using Restaurant.Database.Services;
using Restaurant.Database.Services.Implementations;

namespace Restaurant.Services.Implementations
{
    public class RoleService : IRoleService
    {
        #region Declarations

        private readonly IRoleDbService roleDb;

        #endregion

        #region Constructor

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


        public IEnumerable<Role> GetAllRoles()
        {
            return roleDb.GetAllRoles().Select(i => new Role
            {
                Id = i.Id,
                Name = i.Name
            }).ToList();
        }

        #endregion
    }
}
