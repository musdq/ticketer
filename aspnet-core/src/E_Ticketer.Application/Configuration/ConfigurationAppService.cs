using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using E_Ticketer.Configuration.Dto;

namespace E_Ticketer.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : E_TicketerAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
