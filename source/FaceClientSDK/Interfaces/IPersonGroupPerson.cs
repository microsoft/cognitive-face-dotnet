using FaceClientSDK.Domain.PersonGroupPerson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaceClientSDK.Interfaces
{
    public interface IPersonGroupPerson
    {
        Task<AddFaceResult> AddFaceAsync(string largePersonGroupId, string personId, string url, string userData, string targetFace);

        Task<CreateResult> CreateAsync(string largePersonGroupId, string name, string userData);

        Task<bool> DeleteAsync(string largePersonGroupId, string personId);

        Task<bool> DeleteFaceAsync(string largePersonGroupId, string personId, string persistedFaceId);

        Task<GetResult> GetAsync(string largePersonGroupId, string personId);

        Task<GetFaceResult> GetFaceAsync(string largePersonGroupId, string personId, string persistedFaceId);

        Task<List<ListResult>> ListAsync(string largePersonGroupId, string start, string top);

        Task<bool> UpdateAsync(string largePersonGroupId, string personId, string name, string userData);

        Task<bool> UpdateFaceAsync(string largePersonGroupId, string personId, string persistedFaceId, string userData);
    }
}