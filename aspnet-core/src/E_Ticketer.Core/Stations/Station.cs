using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace E_Ticketer.Stations
{
    [Table("AppStations")]
    public class Station:FullAuditedEntity,IMustHaveTenant
    {
        public const int MaxTitleLength = 128;
        public const int MaxDescriptionLength = 2048;

        public int TenantId { get; set; }

        [Required]
        [StringLength(MaxTitleLength)]
        public virtual string Name { get;  set; }

        [Required]
        [StringLength(MaxTitleLength)]
        public virtual string Lga { get;  set; }

        [Required]
        [StringLength(MaxTitleLength)]
        public virtual string State { get;  set; }
    }
}
