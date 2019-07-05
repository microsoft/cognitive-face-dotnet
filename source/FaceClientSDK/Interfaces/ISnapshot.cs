using FaceClientSDK.Domain.Snapshot;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaceClientSDK.Interfaces
{
    public interface ISnapshot
    {
        Task<bool> ApplyAsync(string snapshotId, string objectId, string mode);   
        
        Task<bool> DeleteAsync(string snapshotId);

        Task<GetResult> GetAsync(string snapshotId);

        Task<GetOperationStatusResult> GetOperationStatusAsync(string snapshotId);    

        Task<List<ListResult>> ListAsync(string type, string applyScope);

        Task<TakeResult> TakeAsync(string type, string objectId, string[] applyScope, string userData);  

        Task<bool> UpdateAsync(string snapshotId, string [] applyScope, string userData);


    }
}
