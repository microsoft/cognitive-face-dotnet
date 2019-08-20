using System.Collections.Generic;

namespace FaceClientSDK.Domain.Face
{
    public class GroupResult
    {
        public List<List<string>> groups { get; set; }
        public List<string> messyGroup { get; set; }
    }
}