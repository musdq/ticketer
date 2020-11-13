using System.Threading.Tasks;
using Abp.Application.Services;
using E_Ticketer.Sessions.Dto;

namespace E_Ticketer.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
