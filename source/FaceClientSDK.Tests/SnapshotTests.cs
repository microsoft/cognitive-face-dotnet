using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FaceClientSDK.Domain.Snapshot;
using FaceClientSDK.Tests.Fixtures;
using Xunit;
using System.Linq;

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
            applyScope = Array.ConvertAll(guidArray, x => x.ToString());
        }       

        [Fact]
        public async void TakeAsync()
        {            
            var objectType = "FaceList";
            var objectId = "source-face-list-id"; 
            var snapshotUserData = "User provided data for the snapshot.";
            
            bool takeSnapshotResult = false;
            try
            {
                takeSnapshotResult = await ApiReference.Instance.Snapshot.TakeAsync(objectType, objectId, applyScope, snapshotUserData);

            }
            catch (Exception)
            {

                throw;
            }

            Assert.True(takeSnapshotResult);
        }
        [Fact]
        public async void ApplyAsync()
        {                        
            var objectId = "target-face-list-id";
            var mode = "CreateNew";

            bool applySnapshot_result = false;

            try
            {
                applySnapshot_result = await ApiReference.Instance.Snapshot.ApplyAsync(identifier, objectId, mode);
                
            }
            catch (Exception)
            {

                throw;
            }

            Assert.True(applySnapshot_result);

        }

        [Fact]    
        public async void DeleteAsync()
        {
            bool result = false;

            GetResult get_result = null;
            try
            {
                get_result = await ApiReference.Instance.Snapshot.GetAsync(identifier);
                result = await ApiReference.Instance.Snapshot.DeleteAsync(identifier);
            }
            catch
            {
                throw;
            }

            Assert.True(result);
        }

        [Fact]
        public async void GetAsync()
        {          
            GetResult result = null;
            try
            {
                result = await ApiReference.Instance.Snapshot.GetAsync(identifier);
            }
            catch
            {
                throw;
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
            var objectType = "FaceList";
            try
            {
                result = await ApiReference.Instance.Snapshot.ListAsync(objectType,applyScope.ToString());
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
            string userData = string.Empty;
            var updateApplyScope = applyScope.Skip(1).ToArray();
        
            try
            {
                result = await ApiReference.Instance.Snapshot.UpdateAsync(identifier, updateApplyScope, userData);
            }
            catch
            {
                throw;
            }

            Assert.True(result);
        }

    }
}
