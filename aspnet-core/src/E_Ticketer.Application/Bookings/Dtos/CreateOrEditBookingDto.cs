using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace E_Ticketer.Bookings.Dtos
{
    public class CreateOrEditBookingDto : EntityDto<int?>
    {

		public int BookingType { get; set; }
		
		
		public int TicketType { get; set; }
		
		
		public double TicketPrice { get; set; }
		
		
		public int status { get; set; }
		
		
		[Required]
		public string FirstName { get; set; }
		
		
		[Required]
		public string LastName { get; set; }
		
		
		[Required]
		public string PhoneNumber { get; set; }
		
		
		[Required]
		public string EmailAddress { get; set; }
		
		

    }
}