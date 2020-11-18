using Abp.Application.Services.Dto;

namespace E_Ticketer.Stations.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}