using Restaurant.Database.Models;

namespace Restaurant.Services.Models.RoleModels
{
    public class RoleDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsChecked { get; set; }
    }
}
