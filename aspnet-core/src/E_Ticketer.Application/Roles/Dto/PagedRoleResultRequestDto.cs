using Abp.Application.Services.Dto;

namespace E_Ticketer.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

