using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using E_Ticketer.DataExporting;
using E_Ticketer.Stations.Dtos;

namespace E_Ticketer.Stations
{
    public interface ITrainsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetTrainForViewDto>> GetAll(GetAllTrainsInput input);

        Task<GetTrainForViewDto> GetTrainForView(Guid id);

		Task<GetTrainForEditOutput> GetTrainForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditTrainDto input);

		Task Delete(EntityDto<Guid> input);

		Task<FileDto> GetTrainsToExcel(GetAllTrainsForExcelInput input);

		
    }
}