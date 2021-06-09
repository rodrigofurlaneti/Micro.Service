using Micro.Service.Disarmer.DownloadingAProcessedFile.Messages;
using Micro.Service.Base.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace Micro.Service.Disarmer.DownloadingAProcessedFile
{
    public interface IDownloadingAProcessedFileClient
    {
        Task<BaseResult<DownloadingAProcessedFileResult>> DownloadingAProcessedFileAsync(DownloadingAProcessedFileParams parameters, CancellationToken cancellationToken);
    }
}
