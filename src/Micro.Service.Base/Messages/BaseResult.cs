using System.IO;

namespace Micro.Service.Base.Messages
{
    public class BaseResult<TResult>
    {
        public string ItemReferenceId { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public Stream File { get; set; }
        public TResult Result { get; set; }
    }
}
