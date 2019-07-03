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

namespace FaceClientSDK.Tests
{
    public class SnapshotTests : IClassFixture<FaceAPISettingsFixture>
    {
        private FaceAPISettingsFixture faceAPISettingsFixture = null;
        private string identifier = null;
        private string[] applyScope = null;

        public SnapshotTests(FaceAPISettingsFixture fixture)
        {
            faceAPISettingsFixture = fixture;

            ApiReference.FaceAPIKey = faceAPISettingsFixture.FaceAPIKey;
            ApiReference.FaceAPIZone = faceAPISettingsFixture.FaceAPIZone;

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
            bool takeSnapshotResult = false;
            List<ListResult> listResult = null;
            var objectType = "PersonGroup";
            
            //Add PersonGroup to transfer
            try
            {
                result = await ApiReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);
                takeSnapshotResult = await ApiReference.Instance.Snapshot.TakeAsync(objectType, identifier, applyScope, identifier);
                listResult = await ApiReference.Instance.Snapshot.ListAsync(objectType, applyScope.First());

            }
            catch (Exception)
            {
                throw;
            }          
           finally
            {
                //Delete PersonGroup
                var deletion_result = await ApiReference.Instance.PersonGroup.DeleteAsync(identifier);
                //Delete Take
                var delete_take = await ApiReference.Instance.Snapshot.DeleteAsync(listResult.First().id);
            }

            Assert.True(takeSnapshotResult);
        }
        [Fact]
        public async void ApplyAsync()
        {
            bool resultPersonGroup = false;
            var takeSnapshotResult = false;
            var objectType = "PersonGroup";
            List<ListResult> listResult = null;
            var mode = "CreateNew";

            bool applySnapshot_result = false;

            try
            {
                var creation_result = await ApiReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);
                
                resultPersonGroup = await ApiReference.Instance.FaceList.CreateAsync(identifier, identifier, identifier);
                takeSnapshotResult = await ApiReference.Instance.Snapshot.TakeAsync(objectType, identifier, applyScope, identifier);
                listResult = await ApiReference.Instance.Snapshot.ListAsync(objectType, applyScope.First());

                applySnapshot_result = await ApiReference.Instance.Snapshot.ApplyAsync(listResult.First().id, identifier, mode);
                
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //Delete PersonGroup
                var deletion_result = await ApiReference.Instance.FaceList.DeleteAsync(identifier);
                //Delete Take
                var delete_take = await ApiReference.Instance.Snapshot.DeleteAsync(listResult.First().id);
            }

            Assert.True(applySnapshot_result);

        }

        [Fact]    
        public async void DeleteAsync()
        {
            bool result = false;
            bool resultPersonGroup = false;
            var takeSnapshotResult = false;
            var objectType = "PersonGroup";
            List<ListResult> listResult = null;

            try
            {
                resultPersonGroup = await ApiReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);
                takeSnapshotResult = await ApiReference.Instance.Snapshot.TakeAsync(objectType, identifier, applyScope, identifier);
                listResult = await ApiReference.Instance.Snapshot.ListAsync(objectType, applyScope.First());

                result = await ApiReference.Instance.Snapshot.DeleteAsync(listResult.First().id);
            }
            catch
            {
                throw;
            }
            finally
            {
                //Delete PersonGroup
                var deletion_result = await ApiReference.Instance.PersonGroup.DeleteAsync(identifier);                
            }

            Assert.True(result);
        }

        [Fact]
        public async void GetAsync()
        {
            bool resultPersonGroup = false;
            GetResult result = null;
            var takeSnapshotResult = false;
            var objectType = "PersonGroup";
            List<ListResult> listResult = null;

            //Add PersonGroup and Take Operation 
            try
            {
                resultPersonGroup = await ApiReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);
                takeSnapshotResult = await ApiReference.Instance.Snapshot.TakeAsync(objectType, identifier, applyScope, identifier);
                listResult = await ApiReference.Instance.Snapshot.ListAsync(objectType, applyScope.First());
                result = await ApiReference.Instance.Snapshot.GetAsync(listResult.First().id);
            }
            catch
            {
                throw;
            }
            finally
            {
                //Delete PersonGroup
                var deletion_result = await ApiReference.Instance.PersonGroup.DeleteAsync(identifier);
                //Delete Take
                var delete_take = await ApiReference.Instance.Snapshot.DeleteAsync(listResult.First().id);
            }

            Assert.True(result != null);

        }

        [Fact]
        public async void GetOperationStatusAsync()
        {
            GetOperationStatusResult result = null;
            try
            {
                result = await ApiReference.Instance.Snapshot.GetOperationStatusAsync(identifier);
            }
            catch
            {
                throw;
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
            var takeSnapshotResult = false;
            var objectType = "PersonGroup";
            List<ListResult> listResult = null;
            try
            {
                resultPersonGroup = await ApiReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);
                takeSnapshotResult = await ApiReference.Instance.Snapshot.TakeAsync(objectType, identifier, applyScope, identifier);
                listResult = await ApiReference.Instance.Snapshot.ListAsync(objectType, applyScope.First());

                result = await ApiReference.Instance.Snapshot.UpdateAsync(listResult.First().id, applyScope.Skip(1).ToArray(), userData);
            }
            catch
            {
                throw;
            }
            finally
            {
                //Delete PersonGroup
                var deletion_result = await ApiReference.Instance.PersonGroup.DeleteAsync(identifier);
                //Delete Take
                var delete_take = await ApiReference.Instance.Snapshot.DeleteAsync(listResult.First().id);
            }

            Assert.True(result);
        }       

    }
}
