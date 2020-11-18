using Abp.Application.Services.Dto;

namespace E_Ticketer.Stations.Dtos
{
    public class GetAllStationsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string NameFilter { get; set; }

		public string LgaFilter { get; set; }

		public string StateFilter { get; set; }



    }
}