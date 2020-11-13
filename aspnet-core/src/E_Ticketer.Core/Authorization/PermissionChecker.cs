using Abp.Authorization;
using E_Ticketer.Authorization.Roles;
using E_Ticketer.Authorization.Users;

namespace E_Ticketer.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
