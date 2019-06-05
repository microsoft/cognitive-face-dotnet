using System;
using System.Collections.Generic;
using System.Text;

namespace FaceClientSDK.Domain.Snapshot
{
    public class GetResult
    {
        public string id { get; set; }

        public string account { get; set; }

        public string type { get; set; }

        public string[] applyScope { get; set; }

        public string userData { get; set; }

        public string createdDateTime { get; set; }

        public string lastUpdatedDateTime { get; set; }
    }
}
