using Micro.Service.Disarmer.CheckTheStatusOfAPositiveSelection.Messages;
using Micro.Service.Base.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace Micro.Service.Disarmer.CheckTheStatusOfAPositiveSelection
{
    public interface ICheckTheStatusOfAPositiveSelectionClient
    {
        Task<BaseResult<CheckTheStatusOfAPositiveSelectionResult>> CheckTheStatusOfAPositiveSelectionAsync(CheckTheStatusOfAPositiveSelectionParams parameters, CancellationToken cancellationToken);
    }
}
