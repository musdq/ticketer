using Abp.Application.Services;
using E_Ticketer.MultiTenancy.Dto;

namespace E_Ticketer.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

