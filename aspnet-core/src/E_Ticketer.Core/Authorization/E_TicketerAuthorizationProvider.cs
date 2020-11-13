using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace E_Ticketer.Authorization
{
    public class E_TicketerAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, E_TicketerConsts.LocalizationSourceName);
        }
    }
}
