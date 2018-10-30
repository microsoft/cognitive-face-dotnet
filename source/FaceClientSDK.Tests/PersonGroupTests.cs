using FaceClientSDK.Domain.PersonGroup;
using FaceClientSDK.Tests.Fixtures;
using System.Collections.Generic;
using Xunit;

namespace FaceClientSDK.Tests
{
    public class PersonGroupTests : IClassFixture<FaceAPISettingsFixture>
    {
        private FaceAPISettingsFixture faceAPISettingsFixture = null;

        public PersonGroupTests(FaceAPISettingsFixture fixture)
        {
            faceAPISettingsFixture = fixture;

            ApiReference.FaceAPIKey = faceAPISettingsFixture.FaceAPIKey;
            ApiReference.FaceAPIZone = faceAPISettingsFixture.FaceAPIZone;
        }

        [Fact]
        public async void CreateAsyncTest()
        {
            bool result = false;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                result = await ApiReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await ApiReference.Instance.PersonGroup.DeleteAsync(identifier);
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
                var creation_result = await ApiReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);
                result = await ApiReference.Instance.PersonGroup.DeleteAsync(identifier);
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
                var creation_result = await ApiReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);

                if (creation_result)
                    result = await ApiReference.Instance.PersonGroup.GetAsync(identifier);
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await ApiReference.Instance.PersonGroup.DeleteAsync(identifier);
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
                var creation_result = await ApiReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);

                bool training_result = false;
                training_result = await ApiReference.Instance.PersonGroup.TrainAsync(identifier);

                if (training_result)
                {
                    while (true)
                    {
                        System.Threading.Tasks.Task.Delay(1000).Wait();
                        result = await ApiReference.Instance.PersonGroup.GetTrainingStatusAsync(identifier);

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
                var deletion_result = await ApiReference.Instance.PersonGroup.DeleteAsync(identifier);
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
                var creation_result = await ApiReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);

                if (creation_result)
                    result = await ApiReference.Instance.PersonGroup.ListAsync(string.Empty, "1000");
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await ApiReference.Instance.PersonGroup.DeleteAsync(identifier);
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
                var creation_result = await ApiReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);
                result = await ApiReference.Instance.PersonGroup.TrainAsync(identifier);

                while (true)
                {
                    System.Threading.Tasks.Task.Delay(1000).Wait();
                    var status = await ApiReference.Instance.PersonGroup.GetTrainingStatusAsync(identifier);

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
                var deletion_result = await ApiReference.Instance.PersonGroup.DeleteAsync(identifier);
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
                var creation_result = await ApiReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);

                if (creation_result)
                    result = await ApiReference.Instance.PersonGroup.UpdateAsync(identifier, "Name", "User Data Sample");
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await ApiReference.Instance.PersonGroup.DeleteAsync(identifier);
            }

            Assert.True(result);
        }
    }
}