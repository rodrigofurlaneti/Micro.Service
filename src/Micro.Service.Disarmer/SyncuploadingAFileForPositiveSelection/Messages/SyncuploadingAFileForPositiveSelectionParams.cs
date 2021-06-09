using System.IO;

namespace Micro.Service.Disarmer.SyncuploadingAFileForPositiveSelection.Messages
{
    public class SyncuploadingAFileForPositiveSelectionParams
    {
        public byte[] BinaryFile { get; set; }
        public string Format { get; set; }
        public string FileName { get; set; }
    }
}
