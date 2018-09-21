using System.Collections.Generic;

namespace FaceClientSDK.Domain.FaceList
{
    public class PersistedFaces
    {
        public string persistedFaceId { get; set; }
        public string userData { get; set; }
    }

    public class GetResult
    {
        public string faceListId { get; set; }
        public string name { get; set; }
        public string userData { get; set; }
        public List<PersistedFaces> persistedFaces { get; set; }
    }
}