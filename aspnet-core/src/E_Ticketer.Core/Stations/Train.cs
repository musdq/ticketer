using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace E_Ticketer.Stations
{
    [Table("AppTrains")]
    public class Train:FullAuditedEntity, IMustHaveTenant
    {
        public Train()
        {
            Status = (int)TrainStatus.Active;
        }
        public int TenantId { get; set; }

        public string Identifier { get; set; }

        public int Status { get; set; }

    }

    enum TrainStatus
    {
        Active,
        Suspended,
        Damaged
    }
}
