using FaceClientSDK.Domain.LargePersonGroup;
using FaceClientSDK.Tests.Fixtures;
using FaceClientSDK.Tests.Helpers;
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
            TimeoutHelper.Timeout = faceAPISettingsFixture.Timeout;
        }

        [Fact]
        public void CreateAsyncTest()
        {
            TimeoutHelper.ThrowExceptionInTimeout(() =>
            {
                bool result = false;
                var identifier = System.Guid.NewGuid().ToString();

                try
                {
                    result = APIReference.Instance.LargePersonGroupInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {result}");
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_result = APIReference.Instance.LargePersonGroupInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_result}");
                }

                Assert.True(result);
            });
        }

        [Fact]
        public void DeleteAsyncTest()
        {
            TimeoutHelper.ThrowExceptionInTimeout(() =>
            {
                bool result = false;
                var identifier = System.Guid.NewGuid().ToString();

                try
                {
                    var creation_result = APIReference.Instance.LargePersonGroupInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_result}");

                    result = APIReference.Instance.LargePersonGroupInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {result}");
                }
                catch
                {
                    throw;
                }

                Assert.True(result);
            });
        }

        [Fact]
        public void GetAsyncTest()
        {
            TimeoutHelper.ThrowExceptionInTimeout(() =>
            {
                GetResult result = null;
                var identifier = System.Guid.NewGuid().ToString();

                try
                {
                    var creation_result = APIReference.Instance.LargePersonGroupInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_result}");

                    if (creation_result)
                        result = APIReference.Instance.LargePersonGroupInstance.GetAsync(identifier).Result;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_result = APIReference.Instance.LargePersonGroupInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_result}");
                }

                Assert.True(result != null);
            });
        }

        [Fact]
        public void GetTrainingStatusAsyncTest()
        {
            TimeoutHelper.ThrowExceptionInTimeout(() =>
            {
                GetTrainingStatusResult result = null;
                var identifier = System.Guid.NewGuid().ToString();

                try
                {
                    var creation_result = APIReference.Instance.LargePersonGroupInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_result}");

                    bool training_result = false;
                    training_result = APIReference.Instance.LargePersonGroupInstance.TrainAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Train Result: {training_result}");

                    if (training_result)
                        result = APIReference.Instance.LargePersonGroupInstance.GetTrainingStatusAsync(identifier).Result;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_result = APIReference.Instance.LargePersonGroupInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_result}");
                }

                Assert.True(result != null);
            });
        }

        [Fact]
        public void ListAsyncTest()
        {
            TimeoutHelper.ThrowExceptionInTimeout(() =>
            {
                List<ListResult> result = null;
                var identifier = System.Guid.NewGuid().ToString();

                try
                {
                    var creation_result = APIReference.Instance.LargePersonGroupInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_result}");

                    if (creation_result)
                        result = APIReference.Instance.LargePersonGroupInstance.ListAsync(string.Empty, 1000).Result;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_result = APIReference.Instance.LargePersonGroupInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_result}");
                }

                Assert.True(result != null);
            });
        }

        [Fact]
        public void TrainAsyncTest()
        {
            TimeoutHelper.ThrowExceptionInTimeout(() =>
            {
                bool result = false;
                var identifier = System.Guid.NewGuid().ToString();

                try
                {
                    var creation_result = APIReference.Instance.LargePersonGroupInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_result}");

                    result = APIReference.Instance.LargePersonGroupInstance.TrainAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Train Result: {result}");
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_result = APIReference.Instance.LargePersonGroupInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_result}");
                }

                Assert.True(result);
            });
        }

        [Fact]
        public void UpdateAsyncTest()
        {
            TimeoutHelper.ThrowExceptionInTimeout(() =>
            {
                bool result = false;
                var identifier = System.Guid.NewGuid().ToString();

                try
                {
                    var creation_result = APIReference.Instance.LargePersonGroupInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_result}");

                    if (creation_result)
                        result = APIReference.Instance.LargePersonGroupInstance.UpdateAsync(identifier, "Name", "User Data Sample").Result;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_result = APIReference.Instance.LargePersonGroupInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_result}");
                }

                Assert.True(result);
            });
        }
    }
}