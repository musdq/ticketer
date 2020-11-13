using System.Threading.Tasks;
using E_Ticketer.Configuration.Dto;

namespace E_Ticketer.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
