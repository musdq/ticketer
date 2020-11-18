using System;

namespace E_Ticketer.Stations.Dtos
{
    public class GetAllTripsForExcelInput
    {
		public string Filter { get; set; }

		public int? MaxOriginStationIdFilter { get; set; }
		public int? MinOriginStationIdFilter { get; set; }

		public int? MaxDestStationIdFilter { get; set; }
		public int? MinDestStationIdFilter { get; set; }

		public DateTime? MaxDepartureTimeFilter { get; set; }
		public DateTime? MinDepartureTimeFilter { get; set; }

		public DateTime? MaxArrivalTimeFilter { get; set; }
		public DateTime? MinArrivalTimeFilter { get; set; }

		public int? MaxMaxVipTicketsFilter { get; set; }
		public int? MinMaxVipTicketsFilter { get; set; }

		public int? MaxMaxOtherTicketsFilter { get; set; }
		public int? MinMaxOtherTicketsFilter { get; set; }

		public int? MaxStatusFilter { get; set; }
		public int? MinStatusFilter { get; set; }


		 public string TrainIdentifierFilter { get; set; }

		 
    }
}