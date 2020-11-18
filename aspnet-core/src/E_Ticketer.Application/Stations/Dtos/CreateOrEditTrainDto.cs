using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace E_Ticketer.Stations.Dtos
{
    public class CreateOrEditTrainDto : EntityDto<int?>
    {

		[Required]
		public string Identifier { get; set; }
		
		
		public int Status { get; set; }
		
		

    }
}