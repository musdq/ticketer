using System.Threading.Tasks;
using Abp.Application.Services;
using E_Ticketer.Authorization.Accounts.Dto;

namespace E_Ticketer.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
