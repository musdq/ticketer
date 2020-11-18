using System;
using Abp.Application.Services.Dto;

namespace E_Ticketer.Stations.Dtos
{
    public class TripDto : EntityDto
    {
		public int OriginStationId { get; set; }

		public int DestStationId { get; set; }

		public DateTime DepartureTime { get; set; }

		public DateTime ArrivalTime { get; set; }

		public int MaxVipTickets { get; set; }

		public int MaxOtherTickets { get; set; }

		public int Status { get; set; }


		 public Guid? TrainId { get; set; }

		 
    }
}