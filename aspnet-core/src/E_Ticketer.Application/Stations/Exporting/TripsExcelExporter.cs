using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using E_Ticketer.DataExporting;
using E_Ticketer.DataExporting.Excel.EpPlus;
using E_Ticketer.Stations.Dtos;
using E_Ticketer.Storage;

namespace E_Ticketer.Stations.Exporting
{
    public class TripsExcelExporter : EpPlusExcelExporterBase, ITripsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public TripsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetTripForViewDto> trips)
        {
            return CreateExcelPackage(
                "Trips.xlsx",
                excelPackage =>
                {
                    
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Trips"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("OriginStationId"),
                        L("DestStationId"),
                        L("DepartureTime"),
                        L("ArrivalTime"),
                        L("MaxVipTickets"),
                        L("MaxOtherTickets"),
                        L("Status"),
                        (L("Train")) + L("Identifier")
                        );

                    AddObjects(
                        sheet, 2, trips,
                        _ => _.Trip.OriginStationId,
                        _ => _.Trip.DestStationId,
                        _ => _timeZoneConverter.Convert(_.Trip.DepartureTime, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.Trip.ArrivalTime, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Trip.MaxVipTickets,
                        _ => _.Trip.MaxOtherTickets,
                        _ => _.Trip.Status,
                        _ => _.TrainIdentifier
                        );

					var departureTimeColumn = sheet.Column(3);
                    departureTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					departureTimeColumn.AutoFit();
					var arrivalTimeColumn = sheet.Column(4);
                    arrivalTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					arrivalTimeColumn.AutoFit();
					
					
                });
        }
    }
}
