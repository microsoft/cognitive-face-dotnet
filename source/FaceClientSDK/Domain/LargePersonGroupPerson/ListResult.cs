using System.Collections.Generic;

namespace FaceClientSDK.Domain.LargePersonGroupPerson
{
    public class ListResult
    {
        public string personId { get; set; }
        public string name { get; set; }
        public string userData { get; set; }
        public List<string> persistedFaceIds { get; set; }
    }
}