using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using E_Ticketer.Stations.Dtos;
using E_Ticketer.Stations.Exporting;
using Microsoft.EntityFrameworkCore;

namespace E_Ticketer.Stations
{
	[AbpAuthorize(AppPermissions.Pages_Trains)]
    public class TrainsAppService : E_TicketerAppServiceBase, ITrainsAppService
    {
		 private readonly IRepository<Train> _trainRepository;
		 private readonly ITrainsExcelExporter _trainsExcelExporter;
		 

		  public TrainsAppService(IRepository<Train> trainRepository, ITrainsExcelExporter trainsExcelExporter ) 
		  {
			_trainRepository = trainRepository;
			_trainsExcelExporter = trainsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetTrainForViewDto>> GetAll(GetAllTrainsInput input)
         {
			
			var filteredTrains = _trainRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Identifier.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.IdentifierFilter),  e => e.Identifier == input.IdentifierFilter)
						.WhereIf(input.MinStatusFilter != null, e => e.Status >= input.MinStatusFilter)
						.WhereIf(input.MaxStatusFilter != null, e => e.Status <= input.MaxStatusFilter);

			var pagedAndFilteredTrains = filteredTrains
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var trains = from o in pagedAndFilteredTrains
                         select new GetTrainForViewDto() {
							Train = new TrainDto
							{
                                Identifier = o.Identifier,
                                Status = o.Status,
                                Id = o.Id
							}
						};

            var totalCount = await filteredTrains.CountAsync();

            return new PagedResultDto<GetTrainForViewDto>(
                totalCount,
                await trains.ToListAsync()
            );
         }
		 
		 public async Task<GetTrainForViewDto> GetTrainForView(int id)
         {
            var train = await _trainRepository.GetAsync(id);

            var output = new GetTrainForViewDto { Train = ObjectMapper.Map<TrainDto>(train) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Trains_Edit)]
		 public async Task<GetTrainForEditOutput> GetTrainForEdit(EntityDto input)
         {
            var train = await _trainRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetTrainForEditOutput {Train = ObjectMapper.Map<CreateOrEditTrainDto>(train)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditTrainDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Trains_Create)]
		 protected virtual async Task Create(CreateOrEditTrainDto input)
         {
            var train = ObjectMapper.Map<Train>(input);

			
			if (AbpSession.TenantId != null)
			{
				train.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _trainRepository.InsertAsync(train);
         }

		 [AbpAuthorize(AppPermissions.Pages_Trains_Edit)]
		 protected virtual async Task Update(CreateOrEditTrainDto input)
         {
            var train = await _trainRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, train);
         }

		 [AbpAuthorize(AppPermissions.Pages_Trains_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _trainRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetTrainsToExcel(GetAllTrainsForExcelInput input)
         {
			
			var filteredTrains = _trainRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Identifier.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.IdentifierFilter),  e => e.Identifier == input.IdentifierFilter)
						.WhereIf(input.MinStatusFilter != null, e => e.Status >= input.MinStatusFilter)
						.WhereIf(input.MaxStatusFilter != null, e => e.Status <= input.MaxStatusFilter);

			var query = (from o in filteredTrains
                         select new GetTrainForViewDto() { 
							Train = new TrainDto
							{
                                Identifier = o.Identifier,
                                Status = o.Status,
                                Id = o.Id
							}
						 });


            var trainListDtos = await query.ToListAsync();

            return _trainsExcelExporter.ExportToFile(trainListDtos);
         }


    }
}