using Abp.Application.Services.Dto;

namespace E_Ticketer.Tickets.Dtos
{
    public class CreateOrEditTicketDto : EntityDto<int?>
    {

		public int TicketType { get; set; }
		
		
		public double Price { get; set; }
		
		

    }
}