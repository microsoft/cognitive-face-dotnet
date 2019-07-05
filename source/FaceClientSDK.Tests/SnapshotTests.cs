using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FaceClientSDK.Domain.Snapshot;
using FaceClientSDK.Tests.Fixtures;
using Xunit;
using System.Linq;
using ListResult = FaceClientSDK.Domain.Snapshot.ListResult;
using GetResult = FaceClientSDK.Domain.Snapshot.GetResult;
using System.Threading.Tasks;

namespace FaceClientSDK.Tests
{
    public class SnapshotTests : IClassFixture<FaceAPISettingsFixture>
    {
        private FaceAPISettingsFixture faceAPISettingsFixture = null;
        private string identifier = null;
        private string[] applyScope = null;
        private string SubscriptionID = null;
        public SnapshotTests(FaceAPISettingsFixture fixture)
        {
            faceAPISettingsFixture = fixture;

            ApiReference.FaceAPIKey = faceAPISettingsFixture.FaceAPIKey;
            ApiReference.FaceAPIZone = faceAPISettingsFixture.FaceAPIZone;
            SubscriptionID = faceAPISettingsFixture.SubscriptionID;

            identifier = Guid.NewGuid().ToString();
            Guid[] guidArray = new Guid[3];
            guidArray[0] = Guid.NewGuid();
            guidArray[1] = Guid.NewGuid();
            guidArray[2] = Guid.NewGuid();

            applyScope = Array.ConvertAll(guidArray, x => x.ToString());            
        }       

        [Fact]
        public async void TakeAsync()
        {
            bool result = false;
            TakeResult takeSnapshotResult = null;
            var objectType = "PersonGroup";
            GetOperationStatusResult operationResult=null;
            
            //Add PersonGroup to transfer
            try
            {
                result = await ApiReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);
                takeSnapshotResult = await ApiReference.Instance.Snapshot.TakeAsync(objectType, identifier, applyScope, identifier);                
                while (true)
                {
                    System.Threading.Tasks.Task.Delay(1000).Wait();
                    operationResult = await ApiReference.Instance.Snapshot.GetOperationStatusAsync(takeSnapshotResult.OperationLocation);

                    if (operationResult.status != "running")
                    {
                        break;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }          
           finally
            {
                var id = operationResult.resourceLocation.Split("/")[2];
                var deleted = DeleteResources(identifier, id);               
            }

            Assert.True(takeSnapshotResult!=null);
        }
        [Fact]
        public async void ApplyAsync()
        {
            bool resultPersonGroup = false;
            var objectType = "PersonGroup";
            var mode = "CreateNew";
            TakeResult takeSnapshotResult = null;          
            GetOperationStatusResult operationResult = null;
            string id = null;
            applyScope[2] = SubscriptionID;
            bool applySnapshot_result = false;


            try
            {
                var creation_result = await ApiReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);

                resultPersonGroup = await ApiReference.Instance.FaceList.CreateAsync(identifier, identifier, identifier);
                takeSnapshotResult = await ApiReference.Instance.Snapshot.TakeAsync(objectType, identifier, applyScope, identifier);
                while (true)
                {
                    operationResult = await ApiReference.Instance.Snapshot.GetOperationStatusAsync(takeSnapshotResult.OperationLocation);

                    if (operationResult.status != "running")
                    {
                        break;
                    }
                    Task.Delay(1000).Wait();
                }
                id = operationResult.resourceLocation.Split("/")[2];
                applySnapshot_result = await ApiReference.Instance.Snapshot.ApplyAsync(id, "new" + identifier, mode);

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                var deleted = DeleteResources(identifier, id);
            }
                Assert.True(applySnapshot_result);

        }

        [Fact]    
        public async void DeleteAsync()
        {
            bool result = false;
            bool resultPersonGroup = false;
            var objectType = "PersonGroup";
            TakeResult takeSnapshotResult = null;                   
            GetOperationStatusResult operationResult = null;
            string id = null;

            try
            {
                resultPersonGroup = await ApiReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);
                takeSnapshotResult = await ApiReference.Instance.Snapshot.TakeAsync(objectType, identifier, applyScope, identifier);
                while (true)
                {
                    operationResult = await ApiReference.Instance.Snapshot.GetOperationStatusAsync(takeSnapshotResult.OperationLocation);

                    if (operationResult.status != "running")
                    {
                        break;
                    }
                    Task.Delay(1000).Wait();
                }
                id = operationResult.resourceLocation.Split("/")[2];
                result = await ApiReference.Instance.Snapshot.DeleteAsync(id);
            }
            catch
            {
                throw;
            }
            finally
            {
                var deleted = DeleteResources(identifier, id);
            }

            Assert.True(result);
        }

        [Fact]
        public async void GetAsync()
        {
            bool resultPersonGroup = false;
            var objectType = "PersonGroup";
            GetResult result = null;
            TakeResult takeSnapshotResult = null;            
            GetOperationStatusResult operationResult = null;
            string id = null;
           

            //Add PersonGroup and Take Operation 
            try
            {
                resultPersonGroup = await ApiReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);
                takeSnapshotResult = await ApiReference.Instance.Snapshot.TakeAsync(objectType, identifier, applyScope, identifier);
                while (true)
                {
                    operationResult = await ApiReference.Instance.Snapshot.GetOperationStatusAsync(takeSnapshotResult.OperationLocation);

                    if (operationResult.status != "running")
                    {
                        break;
                    }
                    Task.Delay(1000).Wait();
                }

                id = operationResult.resourceLocation.Split("/")[2];
                result = await ApiReference.Instance.Snapshot.GetAsync(id);
            }
            catch
            {
                throw;
            }
            finally
            {               
                var deleted = DeleteResources(identifier, id);
            }

            Assert.True(result != null);

        }

        [Fact]
        public async void GetOperationStatusAsync()
        {
            bool personGroupResult = false;
            var objectType = "PersonGroup";
            TakeResult takeSnapshotResult = null;
            GetOperationStatusResult result = null;

            try
            {
                personGroupResult = await ApiReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);
                takeSnapshotResult = await ApiReference.Instance.Snapshot.TakeAsync(objectType, identifier, applyScope, identifier);
                while (true)
                {
                    result = await ApiReference.Instance.Snapshot.GetOperationStatusAsync(takeSnapshotResult.OperationLocation);
                    if (result.status != "running")
                    {
                        break;
                    }
                    Task.Delay(1000).Wait();

                }

            }
            catch
            {
                throw;
            }
            finally
            {                
                var id = result.resourceLocation.Split("/")[2];
                var deleted = DeleteResources(identifier, id);
            }

            Assert.True(result != null);

        }

        [Fact]
        public async void ListAsync()
        {
            List<ListResult> result = null;
            var objectType = "PersonGroup";
            try
            {
                result = await ApiReference.Instance.Snapshot.ListAsync(objectType,String.Empty);
            }
            catch
            {
                throw;
            }

            Assert.True(result != null);

        }       

        [Fact]
        public async void UpdateAsync()
        {
            bool result = false;
            bool resultPersonGroup = false;
            string userData = string.Empty;
            TakeResult takeSnapshotResult = null;
            var objectType = "PersonGroup";
            List<ListResult> listResult = null;
            GetOperationStatusResult operationResult = null;
            try
            {
                resultPersonGroup = await ApiReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);
                takeSnapshotResult = await ApiReference.Instance.Snapshot.TakeAsync(objectType, identifier, applyScope, identifier);
                while (true)
                {
                    System.Threading.Tasks.Task.Delay(1000).Wait();
                    operationResult = await ApiReference.Instance.Snapshot.GetOperationStatusAsync(takeSnapshotResult.OperationLocation);

                    if (operationResult.status != "running")
                    {
                        break;
                    }
                }
                listResult = await ApiReference.Instance.Snapshot.ListAsync(objectType, applyScope.First());

                result = await ApiReference.Instance.Snapshot.UpdateAsync(listResult.First().id, applyScope.Skip(1).ToArray(), userData);
            }
            catch
            {
                throw;
            }
            finally
            {
                var id = operationResult.resourceLocation.Split("/")[2];
                var deleted = DeleteResources(identifier, id);
            }

            Assert.True(result);
        }  
        
        public async Task<bool> DeleteResources(string identifier, string id)
        {           
            //Delete PersonGroup
            var deletion_result = await ApiReference.Instance.PersonGroup.DeleteAsync(identifier);
            //Delete Take
            var delete_take = await ApiReference.Instance.Snapshot.DeleteAsync(id);

            if (deletion_result && delete_take)
                return true;
            else
                return false;
        }

    }
}
