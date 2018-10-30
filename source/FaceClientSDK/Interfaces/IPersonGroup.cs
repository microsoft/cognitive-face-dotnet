using FaceClientSDK.Domain.PersonGroup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaceClientSDK.Interfaces
{
    public interface IPersonGroup
    {
        Task<bool> CreateAsync(string personGroupId, string name, string userData);

        Task<bool> DeleteAsync(string personGroupId);

        Task<GetResult> GetAsync(string personGroupId);

        Task<GetTrainingStatusResult> GetTrainingStatusAsync(string personGroupId);

        Task<List<ListResult>> ListAsync(string start, string top);

        Task<bool> TrainAsync(string personGroupId);

        Task<bool> UpdateAsync(string personGroupId, string name, string userData);
    }
}