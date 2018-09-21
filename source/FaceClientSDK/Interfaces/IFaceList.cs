using FaceClientSDK.Domain.FaceList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaceClientSDK.Interfaces
{
    public interface IFaceList
    {
        Task<AddFaceResult> AddFaceAsync(string largeFaceListId, string url, string userData, string targetFace);

        Task<bool> CreateAsync(string largeFaceListId, string name, string userData);

        Task<bool> DeleteAsync(string largeFaceListId);

        Task<bool> DeleteFaceAsync(string largeFaceListId, string persistedFaceId);

        Task<GetResult> GetAsync(string largeFaceListId);

        Task<List<ListResult>> ListAsync();

        Task<bool> UpdateAsync(string largeFaceListId, string name, string userData);
    }
}