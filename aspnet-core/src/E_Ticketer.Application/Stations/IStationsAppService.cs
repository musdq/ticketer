using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using E_Ticketer.DataExporting;
using E_Ticketer.Stations.Dtos;

namespace E_Ticketer.Stations
{
    public interface IStationsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetStationForViewDto>> GetAll(GetAllStationsInput input);

        Task<GetStationForViewDto> GetStationForView(int id);

		Task<GetStationForEditOutput> GetStationForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditStationDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetStationsToExcel(GetAllStationsForExcelInput input);

		
    }
}