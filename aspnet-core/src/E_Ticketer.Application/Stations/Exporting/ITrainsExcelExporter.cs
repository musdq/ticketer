using System.Collections.Generic;
using E_Ticketer.Stations.Dtos;

namespace E_Ticketer.Stations.Exporting
{
    public interface ITrainsExcelExporter
    {
        FileDto ExportToFile(List<GetTrainForViewDto> trains);
    }
}