using System;
using System.Collections.Generic;
using System.Text;

namespace FaceClientSDK.Domain.Face
{
    public class Candidate
    {
        public string personId { get; set; }
        public double confidence { get; set; }
    }

    public class IdentifyResult
    {
        public string faceId { get; set; }
        public List<Candidate> candidates { get; set; }
    }
}
