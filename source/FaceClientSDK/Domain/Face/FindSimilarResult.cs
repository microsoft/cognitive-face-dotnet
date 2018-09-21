namespace FaceClientSDK.Domain.Face
{
    public class FindSimilarResult
    {
        public string persistedFaceId { get; set; }
        public string faceId { get; set; }
        public float confidence { get; set; }
    }
}