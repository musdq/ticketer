using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using E_Ticketer.Stations;

namespace E_Ticketer.Bookings
{
    [Table("AppBookings")]
    public class Booking:FullAuditedEntity<Guid>,IMustHaveTenant
    {
        public Booking()
        {
            BookingType = (int) BookingTypes.Single;
            Status = (int) BookingStatus.Pending;
        }

        [Required]
        public int BookingType { get; set; }

        public int TenantId { get; set; }

        [Required]
        public int TicketType { get; set; }

        [Required]
        public double TicketPrice { get; set; }

        [Required]
        public int TripId { get; set; }

        public Trip Trip { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }
    }

    enum BookingTypes
    {
        Single,
        Return,
        OpenReturn,
        Annual
    }

    enum BookingStatus
    {
        Cancelled,
        Refunded,
        Active,
        Suspended,
        Pending
    }
}
