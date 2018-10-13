using FaceClientSDK.Domain.LargeFaceList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaceClientSDK.Interfaces
{
    public interface ILargeFaceList
    {
        Task<AddFaceResult> AddFaceAsync(string largeFaceListId, string url, string userData, string targetFace);

        Task<bool> CreateAsync(string largeFaceListId, string name, string userData);

        Task<bool> DeleteAsync(string largeFaceListId);

        Task<bool> DeleteFaceAsync(string largeFaceListId, string persistedFaceId);

        Task<GetResult> GetAsync(string largeFaceListId);

        Task<GetFaceResult> GetFaceAsync(string largeFaceListId, string persistedFaceId);

        Task<GetTrainingStatusResult> GetTrainingStatusAsync(string largeFaceListId);

        Task<List<ListResult>> ListAsync(string start, string top);

        Task<List<ListFaceResult>> ListFaceAsync(string largeFaceListId);

        Task<bool> TrainAsync(string largeFaceListId);

        Task<bool> UpdateAsync(string largeFaceListId, string name, string userData);

        Task<bool> UpdateFaceAsync(string largeFaceListId, string persistedFaceId, string userData);
    }
}