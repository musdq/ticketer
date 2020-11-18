using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using E_Ticketer.Bookings.Dtos;
using E_Ticketer.DataExporting;
using E_Ticketer.DataExporting.Excel.EpPlus;
using E_Ticketer.Storage;

namespace E_Ticketer.Bookings.Exporting
{
    public class BookingsExcelExporter : EpPlusExcelExporterBase, IBookingsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public BookingsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetBookingForViewDto> bookings)
        {
            return CreateExcelPackage(
                "Bookings.xlsx",
                excelPackage =>
                {
                    
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Bookings"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("BookingType"),
                        L("TicketType"),
                        L("TicketPrice"),
                        L("status"),
                        L("FirstName"),
                        L("LastName"),
                        L("PhoneNumber"),
                        L("EmailAddress")
                        );

                    AddObjects(
                        sheet, 2, bookings,
                        _ => _.Booking.BookingType,
                        _ => _.Booking.TicketType,
                        _ => _.Booking.TicketPrice,
                        _ => _.Booking.status,
                        _ => _.Booking.FirstName,
                        _ => _.Booking.LastName,
                        _ => _.Booking.PhoneNumber,
                        _ => _.Booking.EmailAddress
                        );

					
					
                });
        }
    }
}
