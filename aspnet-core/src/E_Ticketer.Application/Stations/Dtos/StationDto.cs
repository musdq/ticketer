using Abp.Application.Services.Dto;

namespace E_Ticketer.Stations.Dtos
{
    public class StationDto : EntityDto
    {
		public string Name { get; set; }

		public string Lga { get; set; }

		public string State { get; set; }



    }
}