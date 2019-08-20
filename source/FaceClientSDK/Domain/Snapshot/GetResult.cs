using System;

namespace FaceClientSDK.Domain.Snapshot
{
    public class GetResult
    {
        public string id { get; set; }

        public string account { get; set; }

        public string type { get; set; }

        public string[] applyScope { get; set; }

        public string userData { get; set; }

        public DateTime createdDateTime { get; set; }

        public DateTime lastUpdatedDateTime { get; set; }
    }
}