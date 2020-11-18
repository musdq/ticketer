using Abp.Application.Services.Dto;

namespace E_Ticketer.Tickets.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}