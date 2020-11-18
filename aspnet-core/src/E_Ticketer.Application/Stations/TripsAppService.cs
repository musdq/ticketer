using System;
using System.Collections.Generic;
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
    public class TripsAppService : E_TicketerAppServiceBase, ITripsAppService
    {
		 private readonly IRepository<Trip> _tripRepository;
		 private readonly ITripsExcelExporter _tripsExcelExporter;
		 private readonly IRepository<Train,Guid> _lookup_trainRepository;
		 

		  public TripsAppService(IRepository<Trip> tripRepository, ITripsExcelExporter tripsExcelExporter , IRepository<Train, Guid> lookup_trainRepository) 
		  {
			_tripRepository = tripRepository;
			_tripsExcelExporter = tripsExcelExporter;
			_lookup_trainRepository = lookup_trainRepository;
		
		  }

		 public async Task<PagedResultDto<GetTripForViewDto>> GetAll(GetAllTripsInput input)
         {
			
			var filteredTrips = _tripRepository.GetAll()
						.Include( e => e.Train)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false )
						.WhereIf(input.MinOriginStationIdFilter != null, e => e.OriginStationId >= input.MinOriginStationIdFilter)
						.WhereIf(input.MaxOriginStationIdFilter != null, e => e.OriginStationId <= input.MaxOriginStationIdFilter)
						.WhereIf(input.MinDestStationIdFilter != null, e => e.DestStationId >= input.MinDestStationIdFilter)
						.WhereIf(input.MaxDestStationIdFilter != null, e => e.DestStationId <= input.MaxDestStationIdFilter)
						.WhereIf(input.MinDepartureTimeFilter != null, e => e.DepartureTime >= input.MinDepartureTimeFilter)
						.WhereIf(input.MaxDepartureTimeFilter != null, e => e.DepartureTime <= input.MaxDepartureTimeFilter)
						.WhereIf(input.MinArrivalTimeFilter != null, e => e.ArrivalTime >= input.MinArrivalTimeFilter)
						.WhereIf(input.MaxArrivalTimeFilter != null, e => e.ArrivalTime <= input.MaxArrivalTimeFilter)
						.WhereIf(input.MinMaxVipTicketsFilter != null, e => e.MaxVipTickets >= input.MinMaxVipTicketsFilter)
						.WhereIf(input.MaxMaxVipTicketsFilter != null, e => e.MaxVipTickets <= input.MaxMaxVipTicketsFilter)
						.WhereIf(input.MinMaxOtherTicketsFilter != null, e => e.MaxOtherTickets >= input.MinMaxOtherTicketsFilter)
						.WhereIf(input.MaxMaxOtherTicketsFilter != null, e => e.MaxOtherTickets <= input.MaxMaxOtherTicketsFilter)
						.WhereIf(input.MinStatusFilter != null, e => e.Status >= input.MinStatusFilter)
						.WhereIf(input.MaxStatusFilter != null, e => e.Status <= input.MaxStatusFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TrainIdentifierFilter), e => e.Train != null && e.Train.Identifier == input.TrainIdentifierFilter);

			var pagedAndFilteredTrips = filteredTrips
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var trips = from o in pagedAndFilteredTrips
                         join o1 in _lookup_trainRepository.GetAll() on o.TrainId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetTripForViewDto() {
							Trip = new TripDto
							{
                                OriginStationId = o.OriginStationId,
                                DestStationId = o.DestStationId,
                                DepartureTime = o.DepartureTime,
                                ArrivalTime = o.ArrivalTime,
                                MaxVipTickets = o.MaxVipTickets,
                                MaxOtherTickets = o.MaxOtherTickets,
                                Status = o.Status,
                                Id = o.Id
							},
                         	TrainIdentifier = s1 == null || s1.Identifier == null ? "" : s1.Identifier.ToString()
						};

            var totalCount = await filteredTrips.CountAsync();

            return new PagedResultDto<GetTripForViewDto>(
                totalCount,
                await trips.ToListAsync()
            );
         }
		 
		 public async Task<GetTripForViewDto> GetTripForView(int id)
         {
            var trip = await _tripRepository.GetAsync(id);

            var output = new GetTripForViewDto { Trip = ObjectMapper.Map<TripDto>(trip) };

		    if (output.Trip.TrainId != null)
            {
                var _lookupTrain = await _lookup_trainRepository.FirstOrDefaultAsync((Guid)output.Trip.TrainId);
                output.TrainIdentifier = _lookupTrain?.Identifier?.ToString();
            }
			
            return output;
         }
         
		 public async Task<GetTripForEditOutput> GetTripForEdit(EntityDto input)
         {
            var trip = await _tripRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetTripForEditOutput {Trip = ObjectMapper.Map<CreateOrEditTripDto>(trip)};

		    if (output.Trip.TrainId != null)
            {
                var _lookupTrain = await _lookup_trainRepository.FirstOrDefaultAsync((Guid)output.Trip.TrainId);
                output.TrainIdentifier = _lookupTrain?.Identifier?.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditTripDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditTripDto input)
         {
            var trip = ObjectMapper.Map<Trip>(input);

			
			if (AbpSession.TenantId != null)
			{
				trip.TenantId = (int) AbpSession.TenantId;
			}
		

            await _tripRepository.InsertAsync(trip);
         }
         
		 protected virtual async Task Update(CreateOrEditTripDto input)
         {
            var trip = await _tripRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, trip);
         }

         public async Task Delete(EntityDto input)
         {
            await _tripRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetTripsToExcel(GetAllTripsForExcelInput input)
         {
			
			var filteredTrips = _tripRepository.GetAll()
						.Include( e => e.Train)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false )
						.WhereIf(input.MinOriginStationIdFilter != null, e => e.OriginStationId >= input.MinOriginStationIdFilter)
						.WhereIf(input.MaxOriginStationIdFilter != null, e => e.OriginStationId <= input.MaxOriginStationIdFilter)
						.WhereIf(input.MinDestStationIdFilter != null, e => e.DestStationId >= input.MinDestStationIdFilter)
						.WhereIf(input.MaxDestStationIdFilter != null, e => e.DestStationId <= input.MaxDestStationIdFilter)
						.WhereIf(input.MinDepartureTimeFilter != null, e => e.DepartureTime >= input.MinDepartureTimeFilter)
						.WhereIf(input.MaxDepartureTimeFilter != null, e => e.DepartureTime <= input.MaxDepartureTimeFilter)
						.WhereIf(input.MinArrivalTimeFilter != null, e => e.ArrivalTime >= input.MinArrivalTimeFilter)
						.WhereIf(input.MaxArrivalTimeFilter != null, e => e.ArrivalTime <= input.MaxArrivalTimeFilter)
						.WhereIf(input.MinMaxVipTicketsFilter != null, e => e.MaxVipTickets >= input.MinMaxVipTicketsFilter)
						.WhereIf(input.MaxMaxVipTicketsFilter != null, e => e.MaxVipTickets <= input.MaxMaxVipTicketsFilter)
						.WhereIf(input.MinMaxOtherTicketsFilter != null, e => e.MaxOtherTickets >= input.MinMaxOtherTicketsFilter)
						.WhereIf(input.MaxMaxOtherTicketsFilter != null, e => e.MaxOtherTickets <= input.MaxMaxOtherTicketsFilter)
						.WhereIf(input.MinStatusFilter != null, e => e.Status >= input.MinStatusFilter)
						.WhereIf(input.MaxStatusFilter != null, e => e.Status <= input.MaxStatusFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TrainIdentifierFilter), e => e.Train != null && e.Train.Identifier == input.TrainIdentifierFilter);

			var query = (from o in filteredTrips
                         join o1 in _lookup_trainRepository.GetAll() on o.TrainId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetTripForViewDto() { 
							Trip = new TripDto
							{
                                OriginStationId = o.OriginStationId,
                                DestStationId = o.DestStationId,
                                DepartureTime = o.DepartureTime,
                                ArrivalTime = o.ArrivalTime,
                                MaxVipTickets = o.MaxVipTickets,
                                MaxOtherTickets = o.MaxOtherTickets,
                                Status = o.Status,
                                Id = o.Id
							},
                         	TrainIdentifier = s1 == null || s1.Identifier == null ? "" : s1.Identifier.ToString()
						 });


            var tripListDtos = await query.ToListAsync();

            return _tripsExcelExporter.ExportToFile(tripListDtos);
         }


         public async Task<PagedResultDto<TripTrainLookupTableDto>> GetAllTrainForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_trainRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Identifier != null && e.Identifier.Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var trainList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<TripTrainLookupTableDto>();
			foreach(var train in trainList){
				lookupTableDtoList.Add(new TripTrainLookupTableDto
				{
					Id = train.Id,
					DisplayName = train.Identifier?.ToString()
				});
			}

            return new PagedResultDto<TripTrainLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}