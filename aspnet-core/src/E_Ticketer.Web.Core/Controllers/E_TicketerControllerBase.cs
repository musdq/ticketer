using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace E_Ticketer.Controllers
{
    public abstract class E_TicketerControllerBase: AbpController
    {
        protected E_TicketerControllerBase()
        {
            LocalizationSourceName = E_TicketerConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
