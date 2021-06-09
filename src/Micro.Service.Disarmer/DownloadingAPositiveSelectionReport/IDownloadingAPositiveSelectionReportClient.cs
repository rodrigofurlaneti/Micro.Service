using Micro.Service.Disarmer.DownloadingAPositiveSelectionReport.Messages;
using Micro.Service.Base.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace Micro.Service.Disarmer.DownloadingAPositiveSelectionReport
{
    public interface IDownloadingAPositiveSelectionReportClient
    {
        Task<BaseResult<DownloadingAPositiveSelectionReportResult>> DownloadingAPositiveSelectionReportAsync(DownloadingAPositiveSelectionReportParams parameters, CancellationToken cancellationToken);
    }
}