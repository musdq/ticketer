namespace E_Ticketer.Tickets.Dtos
{
    public class GetAllTicketsForExcelInput
    {
		public string Filter { get; set; }

		public int? MaxTicketTypeFilter { get; set; }
		public int? MinTicketTypeFilter { get; set; }

		public double? MaxPriceFilter { get; set; }
		public double? MinPriceFilter { get; set; }



    }
}