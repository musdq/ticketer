using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using E_Ticketer.Stations.Dtos;

namespace E_Ticketer.Stations
{
    public interface ITripsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetTripForViewDto>> GetAll(GetAllTripsInput input);

        Task<GetTripForViewDto> GetTripForView(int id);

		Task<GetTripForEditOutput> GetTripForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditTripDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetTripsToExcel(GetAllTripsForExcelInput input);

		
		Task<PagedResultDto<TripTrainLookupTableDto>> GetAllTrainForLookupTable(GetAllForLookupTableInput input);
		
    }
}