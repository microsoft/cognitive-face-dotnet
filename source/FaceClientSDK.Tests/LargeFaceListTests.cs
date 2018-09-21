using FaceClientSDK.Domain.LargeFaceList;
using FaceClientSDK.Tests.Fixtures;
using FaceClientSDK.Tests.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Xunit;

namespace FaceClientSDK.Tests
{
    public class LargeFaceListTests : IClassFixture<FaceAPISettingsFixture>
    {
        private FaceAPISettingsFixture faceAPISettingsFixture = null;

        public LargeFaceListTests(FaceAPISettingsFixture fixture)
        {
            faceAPISettingsFixture = fixture;

            APIReference.FaceAPIKey = faceAPISettingsFixture.FaceAPIKey;
            APIReference.FaceAPIZone = faceAPISettingsFixture.FaceAPIZone;
        }

        [Fact]
        public void AddFaceAsyncTest()
        {
            TimeoutHelper.ThrowExceptionInTimeout(() =>
            {
                AddFaceResult result = null;
                var identifier = System.Guid.NewGuid().ToString();

                try
                {
                    var creation_result = APIReference.Instance.LargeFaceListInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_result}");

                    if (creation_result)
                    {
                        dynamic jUserData = new JObject();
                        jUserData.UserDataSample = "User Data Sample";
                        var rUserData = JsonConvert.SerializeObject(jUserData);

                        result = APIReference.Instance.LargeFaceListInstance.AddFaceAsync(identifier, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty).Result;
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_result = APIReference.Instance.LargeFaceListInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_result}");
                }

                Assert.True(result != null);
            });
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
                    result = APIReference.Instance.LargeFaceListInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {result}");
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_result = APIReference.Instance.LargeFaceListInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_result}");
                }

                Assert.True(result != false);
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
                    var creation_result = APIReference.Instance.LargeFaceListInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_result}");

                    result = APIReference.Instance.LargeFaceListInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {result}");
                }
                catch
                {
                    throw;
                }

                Assert.True(result != false);
            });
        }

        [Fact]
        public void DeleteFaceAsyncTest()
        {
            TimeoutHelper.ThrowExceptionInTimeout(() =>
            {
                bool result = false;
                var identifier = System.Guid.NewGuid().ToString();

                try
                {
                    var creation_result = APIReference.Instance.LargeFaceListInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_result}");

                    AddFaceResult addface_result = null;
                    if (creation_result)
                    {
                        dynamic jUserData = new JObject();
                        jUserData.UserDataSample = "User Data Sample";
                        var rUserData = JsonConvert.SerializeObject(jUserData);

                        addface_result = APIReference.Instance.LargeFaceListInstance.AddFaceAsync(identifier, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty).Result;

                        if (addface_result != null)
                            result = APIReference.Instance.LargeFaceListInstance.DeleteFaceAsync(identifier, addface_result.persistedFaceId).Result;
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_result = APIReference.Instance.LargeFaceListInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_result}");
                }

                Assert.True(result != false);
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
                    var creation_result = APIReference.Instance.LargeFaceListInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_result}");

                    if (creation_result)
                        result = APIReference.Instance.LargeFaceListInstance.GetAsync(identifier).Result;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_result = APIReference.Instance.LargeFaceListInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_result}");
                }

                Assert.True(result != null);
            });
        }

        [Fact]
        public void GetFaceAsyncTest()
        {
            TimeoutHelper.ThrowExceptionInTimeout(() =>
            {
                GetFaceResult result = null;
                var identifier = System.Guid.NewGuid().ToString();

                try
                {
                    var creation_result = APIReference.Instance.LargeFaceListInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_result}");

                    AddFaceResult addface_result = null;
                    if (creation_result)
                    {
                        dynamic jUserData = new JObject();
                        jUserData.UserDataSample = "User Data Sample";
                        var rUserData = JsonConvert.SerializeObject(jUserData);

                        addface_result = APIReference.Instance.LargeFaceListInstance.AddFaceAsync(identifier, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty).Result;

                        if (addface_result != null)
                            result = APIReference.Instance.LargeFaceListInstance.GetFaceAsync(identifier, addface_result.persistedFaceId).Result;
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_result = APIReference.Instance.LargeFaceListInstance.DeleteAsync(identifier).Result;
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
                    var creation_result = APIReference.Instance.LargeFaceListInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_result}");

                    AddFaceResult addface_result = null;
                    if (creation_result)
                    {
                        dynamic jUserData = new JObject();
                        jUserData.UserDataSample = "User Data Sample";
                        var rUserData = JsonConvert.SerializeObject(jUserData);

                        addface_result = APIReference.Instance.LargeFaceListInstance.AddFaceAsync(identifier, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty).Result;

                        if (addface_result != null)
                        {
                            bool training_result = false;
                            training_result = APIReference.Instance.LargeFaceListInstance.TrainAsync(identifier).Result;
                            System.Diagnostics.Trace.Write($"Train Result: {training_result}");

                            if (training_result)
                                result = APIReference.Instance.LargeFaceListInstance.GetTrainingStatusAsync(identifier).Result;
                        }
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_result = APIReference.Instance.LargeFaceListInstance.DeleteAsync(identifier).Result;
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
                    var creation_result = APIReference.Instance.LargeFaceListInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_result}");

                    if (creation_result)
                        result = APIReference.Instance.LargeFaceListInstance.ListAsync(string.Empty, 1000).Result;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_result = APIReference.Instance.LargeFaceListInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_result}");
                }

                Assert.True(result != null);
            });
        }

        [Fact]
        public void ListFaceAsyncTest()
        {
            TimeoutHelper.ThrowExceptionInTimeout(() =>
            {
                List<ListFaceResult> result = null;
                var identifier = System.Guid.NewGuid().ToString();

                try
                {
                    var creation_result = APIReference.Instance.LargeFaceListInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_result}");

                    AddFaceResult addface_result = null;
                    if (creation_result)
                    {
                        dynamic jUserData = new JObject();
                        jUserData.UserDataSample = "User Data Sample";
                        var rUserData = JsonConvert.SerializeObject(jUserData);

                        addface_result = APIReference.Instance.LargeFaceListInstance.AddFaceAsync(identifier, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty).Result;

                        if (addface_result != null)
                            result = APIReference.Instance.LargeFaceListInstance.ListFaceAsync(identifier).Result;
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_result = APIReference.Instance.LargeFaceListInstance.DeleteAsync(identifier).Result;
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
                    var creation_result = APIReference.Instance.LargeFaceListInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_result}");

                    AddFaceResult addface_result = null;
                    if (creation_result)
                    {
                        dynamic jUserData = new JObject();
                        jUserData.UserDataSample = "User Data Sample";
                        var rUserData = JsonConvert.SerializeObject(jUserData);

                        addface_result = APIReference.Instance.LargeFaceListInstance.AddFaceAsync(identifier, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty).Result;

                        if (addface_result != null)
                        {
                            result = APIReference.Instance.LargeFaceListInstance.TrainAsync(identifier).Result;
                            System.Diagnostics.Trace.Write($"Train Result: {result}");
                        }
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_result = APIReference.Instance.LargeFaceListInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_result}");
                }

                Assert.True(result != false);
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
                    var creation_result = APIReference.Instance.LargeFaceListInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_result}");

                    if (creation_result)
                        result = APIReference.Instance.LargeFaceListInstance.UpdateAsync(identifier, "Name", "User Data Sample").Result;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_result = APIReference.Instance.LargeFaceListInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_result}");
                }

                Assert.True(result != false);
            });
        }

        [Fact]
        public void UpdateFaceAsyncTest()
        {
            TimeoutHelper.ThrowExceptionInTimeout(() =>
            {
                bool result = false;
                var identifier = System.Guid.NewGuid().ToString();

                try
                {
                    var creation_result = APIReference.Instance.LargeFaceListInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_result}");

                    AddFaceResult addface_result = null;
                    if (creation_result)
                    {
                        dynamic jUserData = new JObject();
                        jUserData.UserDataSample = "User Data Sample";
                        var rUserData = JsonConvert.SerializeObject(jUserData);

                        addface_result = APIReference.Instance.LargeFaceListInstance.AddFaceAsync(identifier, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty).Result;

                        if (addface_result != null)
                            result = APIReference.Instance.LargeFaceListInstance.UpdateFaceAsync(identifier, addface_result.persistedFaceId, "User Data Sample").Result;
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_result = APIReference.Instance.LargeFaceListInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_result}");
                }

                Assert.True(result != false);
            });
        }
    }
}