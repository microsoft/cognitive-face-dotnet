using FaceClientSDK.Domain.FaceList;
using FaceClientSDK.Tests.Fixtures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Xunit;

namespace FaceClientSDK.Tests
{
    public class FaceListTests : IClassFixture<FaceAPISettingsFixture>
    {
        private FaceAPISettingsFixture faceAPISettingsFixture = null;

        public FaceListTests(FaceAPISettingsFixture fixture)
        {
            faceAPISettingsFixture = fixture;

            APIReference.FaceAPIKey = faceAPISettingsFixture.FaceAPIKey;
            APIReference.FaceAPIZone = faceAPISettingsFixture.FaceAPIZone;
        }

        [Fact]
        public async void AddFaceAsyncTest()
        {
            AddFaceResult result = null;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_result = await APIReference.Instance.FaceList.CreateAsync(identifier, identifier, identifier);

                if (creation_result)
                {
                    dynamic jUserData = new JObject();
                    jUserData.UserDataSample = "User Data Sample";
                    var rUserData = JsonConvert.SerializeObject(jUserData);

                    result = await APIReference.Instance.FaceList.AddFaceAsync(identifier, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.FaceList.DeleteAsync(identifier);
            }

            Assert.True(result != null);
        }

        [Fact]
        public async void CreateAsyncTest()
        {
            bool result = false;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                result = await APIReference.Instance.FaceList.CreateAsync(identifier, identifier, identifier);
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.FaceList.DeleteAsync(identifier);
            }

            Assert.True(result);
        }

        [Fact]
        public async void DeleteAsyncTest()
        {
            bool result = false;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_result = await APIReference.Instance.FaceList.CreateAsync(identifier, identifier, identifier);
                result = await APIReference.Instance.FaceList.DeleteAsync(identifier);
            }
            catch
            {
                throw;
            }

            Assert.True(result);
        }

        [Fact]
        public async void DeleteFaceAsyncTest()
        {
            bool result = false;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_result = await APIReference.Instance.FaceList.CreateAsync(identifier, identifier, identifier);

                AddFaceResult addface_result = null;
                if (creation_result)
                {
                    dynamic jUserData = new JObject();
                    jUserData.UserDataSample = "User Data Sample";
                    var rUserData = JsonConvert.SerializeObject(jUserData);

                    addface_result = await APIReference.Instance.FaceList.AddFaceAsync(identifier, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty);

                    if (addface_result != null)
                        result = await APIReference.Instance.FaceList.DeleteFaceAsync(identifier, addface_result.persistedFaceId);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.FaceList.DeleteAsync(identifier);
            }

            Assert.True(result);
        }

        [Fact]
        public async void GetAsyncTest()
        {
            GetResult result = null;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_result = await APIReference.Instance.FaceList.CreateAsync(identifier, identifier, identifier);

                if (creation_result)
                    result = await APIReference.Instance.FaceList.GetAsync(identifier);
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.FaceList.DeleteAsync(identifier);
            }

            Assert.True(result != null);
        }

        [Fact]
        public async void ListAsyncTest()
        {
            List<ListResult> result = null;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_result = await APIReference.Instance.FaceList.CreateAsync(identifier, identifier, identifier);

                if (creation_result)
                    result = await APIReference.Instance.FaceList.ListAsync();
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.FaceList.DeleteAsync(identifier);
            }

            Assert.True(result != null);
        }

        [Fact]
        public async void UpdateAsyncTest()
        {
            bool result = false;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_result = await APIReference.Instance.FaceList.CreateAsync(identifier, identifier, identifier);

                if (creation_result)
                    result = await APIReference.Instance.FaceList.UpdateAsync(identifier, "Name", "User Data Sample");
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.FaceList.DeleteAsync(identifier);
            }

            Assert.True(result);
        }
    }
}