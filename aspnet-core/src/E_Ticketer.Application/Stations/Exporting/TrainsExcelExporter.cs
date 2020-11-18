using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using E_Ticketer.DataExporting;
using E_Ticketer.DataExporting.Excel.EpPlus;
using E_Ticketer.Stations.Dtos;
using E_Ticketer.Storage;

namespace E_Ticketer.Stations.Exporting
{
    public class TrainsExcelExporter : EpPlusExcelExporterBase, ITrainsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public TrainsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetTrainForViewDto> trains)
        {
            return CreateExcelPackage(
                "Trains.xlsx",
                excelPackage =>
                {
                    
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Trains"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Identifier"),
                        L("Status")
                        );

                    AddObjects(
                        sheet, 2, trains,
                        _ => _.Train.Identifier,
                        _ => _.Train.Status
                        );

					
					
                });
        }
    }
}
