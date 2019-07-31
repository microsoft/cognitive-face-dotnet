using FaceClientSDK.Domain.Face;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaceClientSDK.Interfaces
{
    public interface IFace
    {
        Task<List<DetectResult>> DetectAsync(string url, string returnFaceAttributes, bool returnFaceId = false, bool returnFaceLandmarks = false);

        Task<List<FindSimilarResult>> FindSimilarAsync(string faceId, string faceListId, string largeFaceListId, string[] faceIds, int maxNumOfCandidatesReturned, string mode);

        Task<GroupResult> GroupAsync(string[] faceIds);

        Task<VerifyResult> VerifyAsync(string faceId1, string faceId2, string faceId, string personGroupId, string largePersonGroupId, string personId);

        Task<List<IdentifyResult>> IdentifyAsync(string largePersonGroupId, string[] faceIds, int maxNumOfCandidatesReturned, double confidenceThreshold);
    }
}