using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using E_Ticketer.DataExporting;
using E_Ticketer.Tickets.Dtos;

namespace E_Ticketer.Tickets
{
    public interface ITicketsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetTicketForViewDto>> GetAll(GetAllTicketsInput input);

        Task<GetTicketForViewDto> GetTicketForView(int id);

		Task<GetTicketForEditOutput> GetTicketForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditTicketDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetTicketsToExcel(GetAllTicketsForExcelInput input);

		
    }
}