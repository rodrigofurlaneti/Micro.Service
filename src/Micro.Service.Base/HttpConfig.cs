using System.IO;
using System.Net.Http;
using Micro.Service.Base.ValueObjects;


namespace Micro.Service.Base
{
    public class HttpConfig
    {
        public string Endpoint { get; set; }
        public RequestMultipartType RequestMultipartType { get; set; }
        public HttpMethod HttpMethod { get; set; }
        public object Body { get; set; }
        public byte[] BinaryFile { get; set; }
        public Stream PdfFile { get; set; }
        public string FilePath { get; set; }
        public string Format { get; set; }
        public string FileName { get; set; }
    }
}
