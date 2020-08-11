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

        #region Constructor

        public RoleDbService()
        {
            context = new RestaurantDbContext();
        }

        #endregion

        #region Methods

        public void CreateRole(string name)
        {
            context.Roles.Add(new Role()
            {
                Name = name
            });

            context.SaveChanges();
        }

        public IQueryable<Role> GetAllRoles()
        {
            return context.Roles;
        }

        #endregion
    }
}
