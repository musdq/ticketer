using Abp.Application.Services.Dto;

namespace E_Ticketer.Tickets.Dtos
{
    public class GetAllTicketsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int? MaxTicketTypeFilter { get; set; }
		public int? MinTicketTypeFilter { get; set; }

		public double? MaxPriceFilter { get; set; }
		public double? MinPriceFilter { get; set; }



    }
}