using Abp.Application.Services.Dto;

namespace E_Ticketer.Bookings.Dtos
{
    public class BookingDto : EntityDto
    {
		public int BookingType { get; set; }

		public int TicketType { get; set; }

		public double TicketPrice { get; set; }

		public int status { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string PhoneNumber { get; set; }

		public string EmailAddress { get; set; }



    }
}