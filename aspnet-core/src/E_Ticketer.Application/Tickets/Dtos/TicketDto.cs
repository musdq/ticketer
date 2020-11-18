using Abp.Application.Services.Dto;

namespace E_Ticketer.Tickets.Dtos
{
    public class TicketDto : EntityDto
    {
		public int TicketType { get; set; }

		public double Price { get; set; }



    }
}