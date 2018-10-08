using System.Collections.Generic;

namespace FaceClientSDK.Domain.PersonGroupPerson
{
    public class GetResult
    {
        public string personId { get; set; }
        public List<string> persistedFaceIds { get; set; }
        public string name { get; set; }
        public string userData { get; set; }
    }
}