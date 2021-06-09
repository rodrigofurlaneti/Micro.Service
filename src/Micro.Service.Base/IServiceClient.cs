using System.Threading;
using System.Threading.Tasks;
using Micro.Service.Base.Messages;

namespace Micro.Service.Base
{
    public interface IServiceClient
    {
        Task<BaseResult<TResponse>> CallAsync<TResponse>(HttpConfig httpConfig, CancellationToken cancellationToken);
    }
}
