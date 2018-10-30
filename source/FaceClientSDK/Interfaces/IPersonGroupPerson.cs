using FaceClientSDK.Domain.PersonGroupPerson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaceClientSDK.Interfaces
{
    public interface IPersonGroupPerson
    {
        Task<AddFaceResult> AddFaceAsync(string personGroupId, string personId, string url, string userData, string targetFace);

        Task<CreateResult> CreateAsync(string personGroupId, string name, string userData);

        Task<bool> DeleteAsync(string personGroupId, string personId);

        Task<bool> DeleteFaceAsync(string personGroupId, string personId, string persistedFaceId);

        Task<GetResult> GetAsync(string personGroupId, string personId);

        Task<GetFaceResult> GetFaceAsync(string personGroupId, string personId, string persistedFaceId);

        Task<List<ListResult>> ListAsync(string personGroupId, string start, string top);

        Task<bool> UpdateAsync(string personGroupId, string personId, string name, string userData);

        Task<bool> UpdateFaceAsync(string personGroupId, string personId, string persistedFaceId, string userData);
    }
}