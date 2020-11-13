using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace E_Ticketer.Stations
{
    [Table("AppTrips")]
    public class Trip:FullAuditedEntity,IMustHaveTenant
    {
        public Trip()
        {
            Status = (int) TripStatus.Enroute;
        }

        public int TenantId { get; set; }

        [Required]
        public int OriginStationId { get; set; }

        [Required]
        public int DestStationId { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        //to manage over-crowding
        [Required]
        public int MaxVipTickets { get; set; }

        [Required]
        public int MaxOtherTickets { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        public int TrainId { get; set; }

        public Train Train { get; set; }


    }

    enum TripStatus
    {
        Enroute,
        OnTime,
        Delayed,
        Cancelled,
        Arrived,
        Departed
    }
}
