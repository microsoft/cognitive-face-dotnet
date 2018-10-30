using FaceClientSDK.Domain.FaceList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaceClientSDK.Interfaces
{
    public interface IFaceList
    {
        Task<AddFaceResult> AddFaceAsync(string faceListId, string url, string userData, string targetFace);

        Task<bool> CreateAsync(string faceListId, string name, string userData);

        Task<bool> DeleteAsync(string faceListId);

        Task<bool> DeleteFaceAsync(string faceListId, string persistedFaceId);

        Task<GetResult> GetAsync(string faceListId);

        Task<List<ListResult>> ListAsync();

        Task<bool> UpdateAsync(string faceListId, string name, string userData);
    }
}