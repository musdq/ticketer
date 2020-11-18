using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper;
using E_Ticketer.DataExporting;
using E_Ticketer.Tickets.Dtos;
using E_Ticketer.Tickets.Exporting;
using Microsoft.EntityFrameworkCore;

namespace E_Ticketer.Tickets
{
	
    public class TicketsAppService : E_TicketerAppServiceBase, ITicketsAppService
    {
		 private readonly IRepository<Ticket> _ticketRepository;
		 private readonly ITicketsExcelExporter _ticketsExcelExporter;
		 

		  public TicketsAppService(IRepository<Ticket> ticketRepository, ITicketsExcelExporter ticketsExcelExporter ) 
		  {
			_ticketRepository = ticketRepository;
			_ticketsExcelExporter = ticketsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetTicketForViewDto>> GetAll(GetAllTicketsInput input)
         {
			
			var filteredTickets = _ticketRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false )
						.WhereIf(input.MinTicketTypeFilter != null, e => e.TicketType >= input.MinTicketTypeFilter)
						.WhereIf(input.MaxTicketTypeFilter != null, e => e.TicketType <= input.MaxTicketTypeFilter)
						.WhereIf(input.MinPriceFilter != null, e => e.Price >= input.MinPriceFilter)
						.WhereIf(input.MaxPriceFilter != null, e => e.Price <= input.MaxPriceFilter);

			var pagedAndFilteredTickets = filteredTickets
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var tickets = from o in pagedAndFilteredTickets
                         select new GetTicketForViewDto() {
							Ticket = new TicketDto
							{
                                TicketType = o.TicketType,
                                Price = o.Price,
                                Id = o.Id
							}
						};

            var totalCount = await filteredTickets.CountAsync();

            return new PagedResultDto<GetTicketForViewDto>(
                totalCount,
                await tickets.ToListAsync()
            );
         }
		 
		 public async Task<GetTicketForViewDto> GetTicketForView(int id)
         {
            var ticket = await _ticketRepository.GetAsync(id);

            var output = new GetTicketForViewDto { Ticket = ObjectMapper.Map<TicketDto>(ticket) };
			
            return output;
         }
		 
		
		 public async Task<GetTicketForEditOutput> GetTicketForEdit(EntityDto input)
         {
            var ticket = await _ticketRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetTicketForEditOutput {Ticket = ObjectMapper.Map<CreateOrEditTicketDto>(ticket)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditTicketDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		
		 protected virtual async Task Create(CreateOrEditTicketDto input)
         {
            var ticket = ObjectMapper.Map<Ticket>(input);

			
			if (AbpSession.TenantId != null)
			{
				ticket.TenantId = (int) AbpSession.TenantId;
			}
		

            await _ticketRepository.InsertAsync(ticket);
         }

		 
		 protected virtual async Task Update(CreateOrEditTicketDto input)
         {
            var ticket = await _ticketRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, ticket);
         }

	
         public async Task Delete(EntityDto input)
         {
            await _ticketRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetTicketsToExcel(GetAllTicketsForExcelInput input)
         {
			
			var filteredTickets = _ticketRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false )
						.WhereIf(input.MinTicketTypeFilter != null, e => e.TicketType >= input.MinTicketTypeFilter)
						.WhereIf(input.MaxTicketTypeFilter != null, e => e.TicketType <= input.MaxTicketTypeFilter)
						.WhereIf(input.MinPriceFilter != null, e => e.Price >= input.MinPriceFilter)
						.WhereIf(input.MaxPriceFilter != null, e => e.Price <= input.MaxPriceFilter);

			var query = (from o in filteredTickets
                         select new GetTicketForViewDto() { 
							Ticket = new TicketDto
							{
                                TicketType = o.TicketType,
                                Price = o.Price,
                                Id = o.Id
							}
						 });


            var ticketListDtos = await query.ToListAsync();

            return _ticketsExcelExporter.ExportToFile(ticketListDtos);
         }


    }
}