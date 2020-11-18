using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using E_Ticketer.Bookings.Dtos;
using E_Ticketer.DataExporting;

namespace E_Ticketer.Bookings
{
    public interface IBookingsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetBookingForViewDto>> GetAll(GetAllBookingsInput input);

        Task<GetBookingForViewDto> GetBookingForView(Guid id);

		Task<GetBookingForEditOutput> GetBookingForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditBookingDto input);

		Task Delete(EntityDto<Guid> input);

		Task<FileDto> GetBookingsToExcel(GetAllBookingsForExcelInput input);

		
    }
}