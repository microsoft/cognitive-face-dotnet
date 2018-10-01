using FaceClientSDK.Domain.LargePersonGroupPerson;
using FaceClientSDK.Tests.Fixtures;
using FaceClientSDK.Tests.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Xunit;

namespace FaceClientSDK.Tests
{
    public class LargePersonGroupPersonTests : IClassFixture<FaceAPISettingsFixture>
    {
        private FaceAPISettingsFixture faceAPISettingsFixture = null;

        public LargePersonGroupPersonTests(FaceAPISettingsFixture fixture)
        {
            faceAPISettingsFixture = fixture;

            APIReference.FaceAPIKey = faceAPISettingsFixture.FaceAPIKey;
            APIReference.FaceAPIZone = faceAPISettingsFixture.FaceAPIZone;
            TimeoutHelper.Timeout = faceAPISettingsFixture.Timeout;
        }

        [Fact]
        public void AddFaceAsyncTest()
        {
            TimeoutHelper.ThrowExceptionInTimeout(() =>
            {
                AddFaceResult result = null;
                var identifier = System.Guid.NewGuid().ToString();
                var personId = string.Empty;

                try
                {
                    var creation_group_result = APIReference.Instance.LargePersonGroupInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_group_result}");

                    var creation_person_result = APIReference.Instance.LargePersonGroupPersonInstance.CreateAsync(identifier, identifier, identifier).Result;
                    personId = creation_person_result.personId;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_person_result.personId}");

                    if ((creation_person_result != null) && creation_group_result)
                    {
                        dynamic jUserData = new JObject();
                        jUserData.UserDataSample = "User Data Sample";
                        var rUserData = JsonConvert.SerializeObject(jUserData);

                        result = APIReference.Instance.LargePersonGroupPersonInstance.AddFaceAsync(identifier, creation_person_result.personId, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty).Result;
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_person_result = APIReference.Instance.LargePersonGroupPersonInstance.DeleteAsync(identifier, personId).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_person_result}");

                    var deletion_group_result = APIReference.Instance.LargePersonGroupInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_group_result}");
                }

                Assert.True(result != null);
            });
        }

        [Fact]
        public void CreateAsyncTest()
        {
            TimeoutHelper.ThrowExceptionInTimeout(() =>
            {
                CreateResult result = null;
                var identifier = System.Guid.NewGuid().ToString();

                try
                {
                    var creation_group_result = APIReference.Instance.LargePersonGroupInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_group_result}");

                    if (creation_group_result)
                        result = APIReference.Instance.LargePersonGroupPersonInstance.CreateAsync(identifier, identifier, identifier).Result;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_person_result = APIReference.Instance.LargePersonGroupPersonInstance.DeleteAsync(identifier, result.personId).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_person_result}");

                    var deletion_group_result = APIReference.Instance.LargePersonGroupInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_group_result}");
                }

                Assert.True(result != null);
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
                    var creation_group_result = APIReference.Instance.LargePersonGroupInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_group_result}");

                    var creation_person_result = APIReference.Instance.LargePersonGroupPersonInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_person_result.personId}");

                    result = APIReference.Instance.LargePersonGroupPersonInstance.DeleteAsync(identifier, creation_person_result.personId).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {result}");

                    var deletion_group_result = APIReference.Instance.LargePersonGroupInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_group_result}");
                }
                catch
                {
                    throw;
                }

                Assert.True(result);
            });
        }

        [Fact]
        public void DeleteFaceAsyncTest()
        {
            TimeoutHelper.ThrowExceptionInTimeout(() =>
            {
                bool result = false;
                var identifier = System.Guid.NewGuid().ToString();
                var personId = string.Empty;

                try
                {
                    var creation_group_result = APIReference.Instance.LargePersonGroupInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_group_result}");

                    var creation_person_result = APIReference.Instance.LargePersonGroupPersonInstance.CreateAsync(identifier, identifier, identifier).Result;
                    personId = creation_person_result.personId;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_person_result.personId}");

                    AddFaceResult addface_result = null;
                    if ((creation_person_result != null) && creation_group_result)
                    {
                        dynamic jUserData = new JObject();
                        jUserData.UserDataSample = "User Data Sample";
                        var rUserData = JsonConvert.SerializeObject(jUserData);

                        addface_result = APIReference.Instance.LargePersonGroupPersonInstance.AddFaceAsync(identifier, personId, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty).Result;

                        if (addface_result != null)
                            result = APIReference.Instance.LargePersonGroupPersonInstance.DeleteFaceAsync(identifier, personId, addface_result.persistedFaceId).Result;
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_person_result = APIReference.Instance.LargePersonGroupPersonInstance.DeleteAsync(identifier, personId).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_person_result}");

                    var deletion_group_result = APIReference.Instance.LargePersonGroupInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_group_result}");
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
                var personId = string.Empty;

                try
                {
                    var creation_group_result = APIReference.Instance.LargePersonGroupInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_group_result}");

                    var creation_person_result = APIReference.Instance.LargePersonGroupPersonInstance.CreateAsync(identifier, identifier, identifier).Result;
                    personId = creation_person_result.personId;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_person_result.personId}");

                    if ((creation_person_result != null) && creation_group_result)
                        result = APIReference.Instance.LargePersonGroupPersonInstance.GetAsync(identifier, personId).Result;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_person_result = APIReference.Instance.LargePersonGroupPersonInstance.DeleteAsync(identifier, personId).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_person_result}");

                    var deletion_group_result = APIReference.Instance.LargePersonGroupInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_group_result}");
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
                var personId = string.Empty;

                try
                {
                    var creation_group_result = APIReference.Instance.LargePersonGroupInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_group_result}");

                    var creation_person_result = APIReference.Instance.LargePersonGroupPersonInstance.CreateAsync(identifier, identifier, identifier).Result;
                    personId = creation_person_result.personId;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_person_result.personId}");

                    AddFaceResult addface_result = null;
                    if ((creation_person_result != null) && creation_group_result)
                    {
                        dynamic jUserData = new JObject();
                        jUserData.UserDataSample = "User Data Sample";
                        var rUserData = JsonConvert.SerializeObject(jUserData);

                        addface_result = APIReference.Instance.LargePersonGroupPersonInstance.AddFaceAsync(identifier, personId, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty).Result;

                        if (addface_result != null)
                            result = APIReference.Instance.LargePersonGroupPersonInstance.GetFaceAsync(identifier, personId, addface_result.persistedFaceId).Result;
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_person_result = APIReference.Instance.LargePersonGroupPersonInstance.DeleteAsync(identifier, personId).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_person_result}");

                    var deletion_group_result = APIReference.Instance.LargePersonGroupInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_group_result}");
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
                var personId = string.Empty;

                try
                {
                    var creation_group_result = APIReference.Instance.LargePersonGroupInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_group_result}");

                    var creation_person_result = APIReference.Instance.LargePersonGroupPersonInstance.CreateAsync(identifier, identifier, identifier).Result;
                    personId = creation_person_result.personId;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_person_result.personId}");

                    if ((creation_person_result != null) && creation_group_result)
                        result = APIReference.Instance.LargePersonGroupPersonInstance.ListAsync(identifier, string.Empty, 1000).Result;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_person_result = APIReference.Instance.LargePersonGroupPersonInstance.DeleteAsync(identifier, personId).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_person_result}");

                    var deletion_group_result = APIReference.Instance.LargePersonGroupInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_group_result}");
                }

                Assert.True(result != null);
            });
        }

        [Fact]
        public void UpdateAsyncTest()
        {
            TimeoutHelper.ThrowExceptionInTimeout(() =>
            {
                bool result = false;
                var identifier = System.Guid.NewGuid().ToString();
                var personId = string.Empty;

                try
                {
                    var creation_group_result = APIReference.Instance.LargePersonGroupInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_group_result}");

                    var creation_person_result = APIReference.Instance.LargePersonGroupPersonInstance.CreateAsync(identifier, identifier, identifier).Result;
                    personId = creation_person_result.personId;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_person_result.personId}");

                    if ((creation_person_result != null) && creation_group_result)
                        result = APIReference.Instance.LargePersonGroupPersonInstance.UpdateAsync(identifier, personId, "Name", "User Data Sample").Result;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_person_result = APIReference.Instance.LargePersonGroupPersonInstance.DeleteAsync(identifier, personId).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_person_result}");

                    var deletion_group_result = APIReference.Instance.LargePersonGroupInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_group_result}");
                }

                Assert.True(result);
            });
        }

        [Fact]
        public void UpdateFaceAsyncTest()
        {
            TimeoutHelper.ThrowExceptionInTimeout(() =>
            {
                bool result = false;
                var identifier = System.Guid.NewGuid().ToString();
                var personId = string.Empty;

                try
                {
                    var creation_group_result = APIReference.Instance.LargePersonGroupInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_group_result}");

                    var creation_person_result = APIReference.Instance.LargePersonGroupPersonInstance.CreateAsync(identifier, identifier, identifier).Result;
                    personId = creation_person_result.personId;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_person_result.personId}");

                    AddFaceResult addface_result = null;
                    if ((creation_person_result != null) && creation_group_result)
                    {
                        dynamic jUserData = new JObject();
                        jUserData.UserDataSample = "User Data Sample";
                        var rUserData = JsonConvert.SerializeObject(jUserData);

                        addface_result = APIReference.Instance.LargePersonGroupPersonInstance.AddFaceAsync(identifier, personId, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty).Result;

                        if (addface_result != null)
                            result = APIReference.Instance.LargePersonGroupPersonInstance.UpdateFaceAsync(identifier, personId, addface_result.persistedFaceId, "User Data Sample").Result;
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_person_result = APIReference.Instance.LargePersonGroupPersonInstance.DeleteAsync(identifier, personId).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_person_result}");

                    var deletion_group_result = APIReference.Instance.LargePersonGroupInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_group_result}");
                }

                Assert.True(result);
            });
        }
    }
}