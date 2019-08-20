using System;

namespace FaceClientSDK.Domain.Snapshot
{
    public class GetOperationStatusResult
    {
        public string status { get; set; }
        public DateTime createdDateTime { get; set; }
        public DateTime lastActionDateTime { get; set; }
        public string resourceLocation { get; set; }
        public string message { get; set; }
    }
}