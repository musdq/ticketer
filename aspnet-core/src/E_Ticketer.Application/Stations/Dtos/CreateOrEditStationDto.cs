using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace E_Ticketer.Stations.Dtos
{
    public class CreateOrEditStationDto : EntityDto<int?>
    {

		[Required]
		public string Name { get; set; }
		
		
		[Required]
		public string Lga { get; set; }
		
		
		[Required]
		public string State { get; set; }
		
		

    }
}