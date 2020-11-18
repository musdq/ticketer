using System.Collections.Generic;
using E_Ticketer.DataExporting;
using E_Ticketer.Stations.Dtos;

namespace E_Ticketer.Stations.Exporting
{
    public interface ITripsExcelExporter
    {
        FileDto ExportToFile(List<GetTripForViewDto> trips);
    }
}