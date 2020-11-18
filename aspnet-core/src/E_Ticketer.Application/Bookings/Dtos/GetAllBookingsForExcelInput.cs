namespace E_Ticketer.Bookings.Dtos
{
    public class GetAllBookingsForExcelInput
    {
		public string Filter { get; set; }

		public int? MaxBookingTypeFilter { get; set; }
		public int? MinBookingTypeFilter { get; set; }

		public int? MaxTicketTypeFilter { get; set; }
		public int? MinTicketTypeFilter { get; set; }

		public double? MaxTicketPriceFilter { get; set; }
		public double? MinTicketPriceFilter { get; set; }

		public int? MaxstatusFilter { get; set; }
		public int? MinstatusFilter { get; set; }

		public string FirstNameFilter { get; set; }

		public string LastNameFilter { get; set; }

		public string PhoneNumberFilter { get; set; }

		public string EmailAddressFilter { get; set; }



    }
}