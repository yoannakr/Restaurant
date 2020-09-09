using Restaurant.Database.Models;
using System.Collections.Generic;

namespace Restaurant.Services.Models.User
{
    public class UserDto
    {
        #region Declarations

        private string roles;

        #endregion

        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public ICollection<UserRole> Roles { get; set; } = new HashSet<UserRole>();

        public string StringRoles
        {
            get
            {
                roles = string.Empty;

                List<string> allRolesName = new List<string>();

                foreach (UserRole userRole in Roles)
                {
                    allRolesName.Add(userRole.Role.Name);
                }

                roles = string.Join(", ", allRolesName);

                return roles;
            }
        }

        public ICollection<Database.Models.Payment> Payments { get; set; } = new HashSet<Database.Models.Payment>();

        public string ImageSource
        {
            get => "../Images/user_icon.png";
        }

        #endregion
    }
}
