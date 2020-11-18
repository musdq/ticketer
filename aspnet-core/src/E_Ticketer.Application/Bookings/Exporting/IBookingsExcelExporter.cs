using System.Collections.Generic;
using E_Ticketer.Bookings.Dtos;

namespace E_Ticketer.Bookings.Exporting
{
    public interface IBookingsExcelExporter
    {
        FileDto ExportToFile(List<GetBookingForViewDto> bookings);
    }
}