using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using E_Ticketer.MultiTenancy;

namespace E_Ticketer.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
