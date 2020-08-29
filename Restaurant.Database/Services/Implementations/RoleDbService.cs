using System.Linq;
using Restaurant.Database.Data;
using Restaurant.Database.Models;

namespace Restaurant.Database.Services.Implementations
{
    public class RoleDbService : IRoleDbService
    {
        #region Declarations

        private readonly RestaurantDbContext context;

        #endregion

        #region Constructors

        public RoleDbService()
        {
            context = new RestaurantDbContext();
        }

        #endregion

        #region Methods

        public Role CreateRole(string name)
        {
            Role role = new Role()
            {
                Name = name
            };

            context.Roles.Add(role);

            context.SaveChanges();

            return role;
        }

        public IQueryable<Role> GetAllRoles()
        {
            return context.Roles;
        }

        #endregion
    }
}
