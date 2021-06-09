namespace Micro.Service.Disarmer.UploadAFileForPositiveSelection.Messages
{
    public class UploadAFileForPositiveSelectionParams
    {
        public byte[] BinaryFile { get; set; }
        public string Format { get; set; }
        public string FileName { get; set; }
    }
}