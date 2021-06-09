using Micro.Service.Disarmer.SyncuploadingAFileForPositiveSelection.Messages;
using Micro.Service.Base.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace Micro.Service.Disarmer.SyncuploadingAFileForPositiveSelection
{
    public interface ISyncuploadingAFileForPositiveSelectionClient
    {
        Task<BaseResult<SyncuploadingAFileForPositiveSelectionResult>> SyncuploadingAFileForPositiveSelectionAsync(SyncuploadingAFileForPositiveSelectionParams parameters, CancellationToken cancellationToken);
    }
}
