using System.Net.Http;

namespace Micro.Service.Base.Messages
{
    public class ServiceHttpMessage
    {
        public string Endpoint { get; set; }
        public HttpMethod RequestType { get; set; }
        public object Body { get; set; }
    }
}
