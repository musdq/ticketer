using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using E_Ticketer.DataExporting;
using E_Ticketer.DataExporting.Excel.EpPlus;
using E_Ticketer.Stations.Dtos;
using E_Ticketer.Storage;

namespace E_Ticketer.Stations.Exporting
{
    public class StationsExcelExporter : EpPlusExcelExporterBase, IStationsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public StationsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetStationForViewDto> stations)
        {
            return CreateExcelPackage(
                "Stations.xlsx",
                excelPackage =>
                {
                    
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Stations"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Name"),
                        L("Lga"),
                        L("State")
                        );

                    AddObjects(
                        sheet, 2, stations,
                        _ => _.Station.Name,
                        _ => _.Station.Lga,
                        _ => _.Station.State
                        );

					
					
                });
        }
    }
}
