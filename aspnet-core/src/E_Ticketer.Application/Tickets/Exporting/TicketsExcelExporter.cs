using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using E_Ticketer.Tickets.Dtos;

namespace E_Ticketer.Tickets.Exporting
{
    public class TicketsExcelExporter : EpPlusExcelExporterBase, ITicketsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public TicketsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetTicketForViewDto> tickets)
        {
            return CreateExcelPackage(
                "Tickets.xlsx",
                excelPackage =>
                {
                    
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Tickets"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("TicketType"),
                        L("Price")
                        );

                    AddObjects(
                        sheet, 2, tickets,
                        _ => _.Ticket.TicketType,
                        _ => _.Ticket.Price
                        );

					
					
                });
        }
    }
}
