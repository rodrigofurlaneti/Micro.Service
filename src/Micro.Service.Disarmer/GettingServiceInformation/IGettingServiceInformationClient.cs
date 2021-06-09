using Micro.Service.Disarmer.GettingServiceInformation.Messages;
using Micro.Service.Base.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace Micro.Service.Disarmer.GettingServiceInformation
{
    public interface IGettingServiceInformationClient
    {
        Task<BaseResult<GettingServiceInformationResult>> GettingServiceInformationAsync(CancellationToken cancellationToken);
    }
}
