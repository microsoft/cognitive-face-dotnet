using System;
using System.Collections.Generic;
using System.Text;

namespace FaceClientSDK.Domain.Snapshot
{
    public class GetOperationStatusResult
    {
        public string status { get; set; }
        public string createdDateTime { get; set; }
        public string lastActionDateTime { get; set; }
        public string resourceLocation { get; set; }
        public string message { get; set; }
    }
}
