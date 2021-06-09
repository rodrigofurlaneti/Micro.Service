using Micro.Service.Disarmer.UploadAFileForPositiveSelection.Messages;
using Micro.Service.Base.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace Micro.Service.Disarmer.UploadAFileForPositiveSelection
{
    public interface IUploadAFileForPositiveSelectionClient
    {
        Task<BaseResult<UploadAFileForPositiveSelectionResult>> UploadAFileForPositiveSelectionAsync(UploadAFileForPositiveSelectionParams parameters, CancellationToken cancellationToken);
    }
}
