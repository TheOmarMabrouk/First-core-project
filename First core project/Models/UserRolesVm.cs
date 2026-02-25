using Microsoft.AspNetCore.Identity;

namespace First_core_project.Models
{
    public class UserRolesVm
    {
        public UserRolesVm()
        {
            UserRoles = new List<string>();
        }

        public IdentityUser user { get; set; }

        public List<string> UserRoles { get; set; }
    }
}
