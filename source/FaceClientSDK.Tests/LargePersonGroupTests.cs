using FaceClientSDK.Domain.LargePersonGroup;
using FaceClientSDK.Tests.Fixtures;
using System.Collections.Generic;
using Xunit;

namespace FaceClientSDK.Tests
{
    public class LargePersonGroupTests : IClassFixture<FaceAPISettingsFixture>
    {
        private FaceAPISettingsFixture faceAPISettingsFixture = null;

        public LargePersonGroupTests(FaceAPISettingsFixture fixture)
        {
            faceAPISettingsFixture = fixture;

            APIReference.FaceAPIKey = faceAPISettingsFixture.FaceAPIKey;
            APIReference.FaceAPIZone = faceAPISettingsFixture.FaceAPIZone;
        }

        [Fact]
        public async void CreateAsyncTest()
        {
            bool result = false;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                result = await APIReference.Instance.LargePersonGroup.CreateAsync(identifier, identifier, identifier);
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.LargePersonGroup.DeleteAsync(identifier);
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
                var creation_result = await APIReference.Instance.LargePersonGroup.CreateAsync(identifier, identifier, identifier);
                result = await APIReference.Instance.LargePersonGroup.DeleteAsync(identifier);
            }
            catch
            {
                throw;
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
                var creation_result = await APIReference.Instance.LargePersonGroup.CreateAsync(identifier, identifier, identifier);

                if (creation_result)
                    result = await APIReference.Instance.LargePersonGroup.GetAsync(identifier);
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.LargePersonGroup.DeleteAsync(identifier);
            }

            Assert.True(result != null);
        }

        [Fact]
        public async void GetTrainingStatusAsyncTest()
        {
            GetTrainingStatusResult result = null;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_result = await APIReference.Instance.LargePersonGroup.CreateAsync(identifier, identifier, identifier);

                bool training_result = false;
                training_result = await APIReference.Instance.LargePersonGroup.TrainAsync(identifier);

                if (training_result)
                {
                    while (true)
                    {
                        System.Threading.Tasks.Task.Delay(1000).Wait();
                        result = await APIReference.Instance.LargePersonGroup.GetTrainingStatusAsync(identifier);

                        if (result.status != "running")
                        {
                            break;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.LargePersonGroup.DeleteAsync(identifier);
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
                var creation_result = await APIReference.Instance.LargePersonGroup.CreateAsync(identifier, identifier, identifier);

                if (creation_result)
                    result = await APIReference.Instance.LargePersonGroup.ListAsync(string.Empty, 1000);
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.LargePersonGroup.DeleteAsync(identifier);
            }

            Assert.True(result != null);
        }

        [Fact]
        public async void TrainAsyncTest()
        {
            bool result = false;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_result = await APIReference.Instance.LargePersonGroup.CreateAsync(identifier, identifier, identifier);
                result = await APIReference.Instance.LargePersonGroup.TrainAsync(identifier);

                while (true)
                {
                    System.Threading.Tasks.Task.Delay(1000).Wait();
                    var status = await APIReference.Instance.LargePersonGroup.GetTrainingStatusAsync(identifier);

                    if (status.status != "running")
                    {
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.LargePersonGroup.DeleteAsync(identifier);
            }

            Assert.True(result);
        }

        [Fact]
        public async void UpdateAsyncTest()
        {
            bool result = false;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_result = await APIReference.Instance.LargePersonGroup.CreateAsync(identifier, identifier, identifier);

                if (creation_result)
                    result = await APIReference.Instance.LargePersonGroup.UpdateAsync(identifier, "Name", "User Data Sample");
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.LargePersonGroup.DeleteAsync(identifier);
            }

            Assert.True(result);
        }
    }
}