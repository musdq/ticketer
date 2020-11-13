using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace E_Ticketer.Tickets
{
    [Table("AppTickets")]
    public class Ticket:FullAuditedEntity,IMustHaveTenant
    {
        public Ticket()
        {
            TicketType = (int) Types.Regular;
        }

        public int TenantId { get; set; }

        [Required]
        public int TicketType { get; set; }

        [Required]
        public double Price { get; set; }
        
    }

    enum Types
    {
        Regular,
        Vip,
        Discount,
        Children,
        Elderly
    }
}
