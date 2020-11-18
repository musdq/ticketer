using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using E_Ticketer.DataExporting;
using E_Ticketer.Stations.Dtos;
using E_Ticketer.Stations.Exporting;
using Microsoft.EntityFrameworkCore;

namespace E_Ticketer.Stations
{
    public class StationsAppService : E_TicketerAppServiceBase, IStationsAppService
    {
		 private readonly IRepository<Station> _stationRepository;
		 private readonly IStationsExcelExporter _stationsExcelExporter;
		 

		  public StationsAppService(IRepository<Station> stationRepository, IStationsExcelExporter stationsExcelExporter ) 
		  {
			_stationRepository = stationRepository;
			_stationsExcelExporter = stationsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetStationForViewDto>> GetAll(GetAllStationsInput input)
         {
			
			var filteredStations = _stationRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter) || e.Lga.Contains(input.Filter) || e.State.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.LgaFilter),  e => e.Lga == input.LgaFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.StateFilter),  e => e.State == input.StateFilter);

			var pagedAndFilteredStations = filteredStations
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var stations = from o in pagedAndFilteredStations
                         select new GetStationForViewDto() {
							Station = new StationDto
							{
                                Name = o.Name,
                                Lga = o.Lga,
                                State = o.State,
                                Id = o.Id
							}
						};

            var totalCount = await filteredStations.CountAsync();

            return new PagedResultDto<GetStationForViewDto>(
                totalCount,
                await stations.ToListAsync()
            );
         }
		 
		 public async Task<GetStationForViewDto> GetStationForView(int id)
         {
            var station = await _stationRepository.GetAsync(id);

            var output = new GetStationForViewDto { Station = ObjectMapper.Map<StationDto>(station) };
			
            return output;
         }
         
		 public async Task<GetStationForEditOutput> GetStationForEdit(EntityDto input)
         {
            var station = await _stationRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetStationForEditOutput {Station = ObjectMapper.Map<CreateOrEditStationDto>(station)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditStationDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditStationDto input)
         {
            var station = ObjectMapper.Map<Station>(input);

			
			if (AbpSession.TenantId != null)
			{
				station.TenantId = (int) AbpSession.TenantId;
			}
		

            await _stationRepository.InsertAsync(station);
         }

		 protected virtual async Task Update(CreateOrEditStationDto input)
         {
            var station = await _stationRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, station);
         }

         public async Task Delete(EntityDto input)
         {
            await _stationRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetStationsToExcel(GetAllStationsForExcelInput input)
         {
			
			var filteredStations = _stationRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter) || e.Lga.Contains(input.Filter) || e.State.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.LgaFilter),  e => e.Lga == input.LgaFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.StateFilter),  e => e.State == input.StateFilter);

			var query = (from o in filteredStations
                         select new GetStationForViewDto() { 
							Station = new StationDto
							{
                                Name = o.Name,
                                Lga = o.Lga,
                                State = o.State,
                                Id = o.Id
							}
						 });


            var stationListDtos = await query.ToListAsync();

            return _stationsExcelExporter.ExportToFile(stationListDtos);
         }


    }
}