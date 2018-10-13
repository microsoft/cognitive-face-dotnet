using FaceClientSDK.Domain.PersonGroup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaceClientSDK.Interfaces
{
    public interface IPersonGroup
    {
        Task<bool> CreateAsync(string largePersonGroupId, string name, string userData);

        Task<bool> DeleteAsync(string largePersonGroupId);

        Task<GetResult> GetAsync(string largePersonGroupId);

        Task<GetTrainingStatusResult> GetTrainingStatusAsync(string largePersonGroupId);

        Task<List<ListResult>> ListAsync(string start, string top);

        Task<bool> TrainAsync(string largePersonGroupId);

        Task<bool> UpdateAsync(string largePersonGroupId, string name, string userData);
    }
}