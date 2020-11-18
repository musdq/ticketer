using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using E_Ticketer.Bookings.Dtos;
using E_Ticketer.Bookings.Exporting;
using Microsoft.EntityFrameworkCore;

namespace E_Ticketer.Bookings
{
	[AbpAuthorize(AppPermissions.Pages_Bookings)]
    public class BookingsAppService : E_TicketerAppServiceBase, IBookingsAppService
    {
		 private readonly IRepository<Booking> _bookingRepository;
		 private readonly IBookingsExcelExporter _bookingsExcelExporter;
		 

		  public BookingsAppService(IRepository<Booking> bookingRepository, IBookingsExcelExporter bookingsExcelExporter ) 
		  {
			_bookingRepository = bookingRepository;
			_bookingsExcelExporter = bookingsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetBookingForViewDto>> GetAll(GetAllBookingsInput input)
         {
			
			var filteredBookings = _bookingRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.FirstName.Contains(input.Filter) || e.LastName.Contains(input.Filter) || e.PhoneNumber.Contains(input.Filter) || e.EmailAddress.Contains(input.Filter))
						.WhereIf(input.MinBookingTypeFilter != null, e => e.BookingType >= input.MinBookingTypeFilter)
						.WhereIf(input.MaxBookingTypeFilter != null, e => e.BookingType <= input.MaxBookingTypeFilter)
						.WhereIf(input.MinTicketTypeFilter != null, e => e.TicketType >= input.MinTicketTypeFilter)
						.WhereIf(input.MaxTicketTypeFilter != null, e => e.TicketType <= input.MaxTicketTypeFilter)
						.WhereIf(input.MinTicketPriceFilter != null, e => e.TicketPrice >= input.MinTicketPriceFilter)
						.WhereIf(input.MaxTicketPriceFilter != null, e => e.TicketPrice <= input.MaxTicketPriceFilter)
						.WhereIf(input.MinstatusFilter != null, e => e.status >= input.MinstatusFilter)
						.WhereIf(input.MaxstatusFilter != null, e => e.status <= input.MaxstatusFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.FirstNameFilter),  e => e.FirstName == input.FirstNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.LastNameFilter),  e => e.LastName == input.LastNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNumberFilter),  e => e.PhoneNumber == input.PhoneNumberFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.EmailAddressFilter),  e => e.EmailAddress == input.EmailAddressFilter);

			var pagedAndFilteredBookings = filteredBookings
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var bookings = from o in pagedAndFilteredBookings
                         select new GetBookingForViewDto() {
							Booking = new BookingDto
							{
                                BookingType = o.BookingType,
                                TicketType = o.TicketType,
                                TicketPrice = o.TicketPrice,
                                status = o.status,
                                FirstName = o.FirstName,
                                LastName = o.LastName,
                                PhoneNumber = o.PhoneNumber,
                                EmailAddress = o.EmailAddress,
                                Id = o.Id
							}
						};

            var totalCount = await filteredBookings.CountAsync();

            return new PagedResultDto<GetBookingForViewDto>(
                totalCount,
                await bookings.ToListAsync()
            );
         }
		 
		 public async Task<GetBookingForViewDto> GetBookingForView(int id)
         {
            var booking = await _bookingRepository.GetAsync(id);

            var output = new GetBookingForViewDto { Booking = ObjectMapper.Map<BookingDto>(booking) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Bookings_Edit)]
		 public async Task<GetBookingForEditOutput> GetBookingForEdit(EntityDto input)
         {
            var booking = await _bookingRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetBookingForEditOutput {Booking = ObjectMapper.Map<CreateOrEditBookingDto>(booking)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditBookingDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Bookings_Create)]
		 protected virtual async Task Create(CreateOrEditBookingDto input)
         {
            var booking = ObjectMapper.Map<Booking>(input);

			
			if (AbpSession.TenantId != null)
			{
				booking.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _bookingRepository.InsertAsync(booking);
         }

		 [AbpAuthorize(AppPermissions.Pages_Bookings_Edit)]
		 protected virtual async Task Update(CreateOrEditBookingDto input)
         {
            var booking = await _bookingRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, booking);
         }

		 [AbpAuthorize(AppPermissions.Pages_Bookings_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _bookingRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetBookingsToExcel(GetAllBookingsForExcelInput input)
         {
			
			var filteredBookings = _bookingRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.FirstName.Contains(input.Filter) || e.LastName.Contains(input.Filter) || e.PhoneNumber.Contains(input.Filter) || e.EmailAddress.Contains(input.Filter))
						.WhereIf(input.MinBookingTypeFilter != null, e => e.BookingType >= input.MinBookingTypeFilter)
						.WhereIf(input.MaxBookingTypeFilter != null, e => e.BookingType <= input.MaxBookingTypeFilter)
						.WhereIf(input.MinTicketTypeFilter != null, e => e.TicketType >= input.MinTicketTypeFilter)
						.WhereIf(input.MaxTicketTypeFilter != null, e => e.TicketType <= input.MaxTicketTypeFilter)
						.WhereIf(input.MinTicketPriceFilter != null, e => e.TicketPrice >= input.MinTicketPriceFilter)
						.WhereIf(input.MaxTicketPriceFilter != null, e => e.TicketPrice <= input.MaxTicketPriceFilter)
						.WhereIf(input.MinstatusFilter != null, e => e.status >= input.MinstatusFilter)
						.WhereIf(input.MaxstatusFilter != null, e => e.status <= input.MaxstatusFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.FirstNameFilter),  e => e.FirstName == input.FirstNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.LastNameFilter),  e => e.LastName == input.LastNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNumberFilter),  e => e.PhoneNumber == input.PhoneNumberFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.EmailAddressFilter),  e => e.EmailAddress == input.EmailAddressFilter);

			var query = (from o in filteredBookings
                         select new GetBookingForViewDto() { 
							Booking = new BookingDto
							{
                                BookingType = o.BookingType,
                                TicketType = o.TicketType,
                                TicketPrice = o.TicketPrice,
                                status = o.status,
                                FirstName = o.FirstName,
                                LastName = o.LastName,
                                PhoneNumber = o.PhoneNumber,
                                EmailAddress = o.EmailAddress,
                                Id = o.Id
							}
						 });


            var bookingListDtos = await query.ToListAsync();

            return _bookingsExcelExporter.ExportToFile(bookingListDtos);
         }


    }
}