using System;
using Abp.Application.Services.Dto;

namespace E_Ticketer.Stations.Dtos
{
    public class TrainDto : EntityDto<Guid>
    {
		public string Identifier { get; set; }

		public int Status { get; set; }



    }
}