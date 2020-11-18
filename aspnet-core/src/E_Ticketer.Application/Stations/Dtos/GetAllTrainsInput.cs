using Abp.Application.Services.Dto;

namespace E_Ticketer.Stations.Dtos
{
    public class GetAllTrainsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string IdentifierFilter { get; set; }

		public int? MaxStatusFilter { get; set; }
		public int? MinStatusFilter { get; set; }



    }
}