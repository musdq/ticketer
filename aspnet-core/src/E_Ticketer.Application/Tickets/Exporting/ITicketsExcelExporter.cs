using System.Collections.Generic;
using E_Ticketer.Tickets.Dtos;

namespace E_Ticketer.Tickets.Exporting
{
    public interface ITicketsExcelExporter
    {
        FileDto ExportToFile(List<GetTicketForViewDto> tickets);
    }
}