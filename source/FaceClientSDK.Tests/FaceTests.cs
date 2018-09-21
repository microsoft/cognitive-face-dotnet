using FaceClientSDK.Domain.Face;
using DomainFaceList = FaceClientSDK.Domain.FaceList;
using DomainLargePersonGroupPerson = FaceClientSDK.Domain.LargePersonGroupPerson;
using FaceClientSDK.Tests.Fixtures;
using FaceClientSDK.Tests.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Xunit;

namespace FaceClientSDK.Tests
{
    public class FaceTests : IClassFixture<FaceAPISettingsFixture>
    {
        private FaceAPISettingsFixture faceAPISettingsFixture = null;

        public FaceTests(FaceAPISettingsFixture fixture)
        {
            faceAPISettingsFixture = fixture;

            APIReference.FaceAPIKey = faceAPISettingsFixture.FaceAPIKey;
            APIReference.FaceAPIZone = faceAPISettingsFixture.FaceAPIZone;
            TimeoutHelper.Timeout = faceAPISettingsFixture.Timeout;
        }

        [Fact]
        public void DetectAsyncTest()
        {
            TimeoutHelper.ThrowExceptionInTimeout(() =>
            {
                List<DetectResult> result = null;

                try
                {
                    result = APIReference.Instance.FaceInstance.DetectAsync(faceAPISettingsFixture.TestImageUrl, "age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise", true, true).Result;
                }
                catch
                {
                    throw;
                }

                Assert.True(result != null);
            });
        }

        [Fact]
        public void FindSimilarAsyncTest()
        {
            TimeoutHelper.ThrowExceptionInTimeout(() =>
            {
                List<FindSimilarResult> result = null;
                var identifier = System.Guid.NewGuid().ToString();

                try
                {
                    var creation_result = APIReference.Instance.FaceListInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_result}");

                    DomainFaceList.AddFaceResult addface_result = null;
                    if (creation_result)
                    {
                        dynamic jUserData = new JObject();
                        jUserData.UserDataSample = "User Data Sample";
                        var rUserData = JsonConvert.SerializeObject(jUserData);

                        addface_result = APIReference.Instance.FaceListInstance.AddFaceAsync(identifier, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty).Result;

                        if (addface_result != null)
                        {
                            List<DetectResult> detection_result = APIReference.Instance.FaceInstance.DetectAsync(faceAPISettingsFixture.TestImageUrl, "age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise", true, true).Result;

                            if (detection_result != null)
                                result = APIReference.Instance.FaceInstance.FindSimilarAsync(detection_result[0].faceId, identifier, string.Empty, new string[] { }, 10, "matchPerson").Result;
                        }
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    var deletion_result = APIReference.Instance.FaceListInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_result}");
                }

                Assert.True(result != null);
            });
        }

        [Fact]
        public void VerifyAsyncTest()
        {
            TimeoutHelper.ThrowExceptionInTimeout(() =>
            {
                VerifyResult result = null;
                var identifier = System.Guid.NewGuid().ToString();
                var personId = string.Empty;
                try
                {
                    var creation_group_result = APIReference.Instance.LargePersonGroupInstance.CreateAsync(identifier, identifier, identifier).Result;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_group_result}");

                    var creation_person_result = APIReference.Instance.LargePersonGroupPersonInstance.CreateAsync(identifier, identifier, identifier).Result;
                    personId = creation_person_result.personId;
                    System.Diagnostics.Trace.Write($"Creation Result: {creation_person_result.personId}");

                    DomainLargePersonGroupPerson.AddFaceResult addface_result = null;
                    if ((creation_person_result != null) && creation_group_result)
                    {
                        dynamic jUserData = new JObject();
                        jUserData.UserDataSample = "User Data Sample";
                        var rUserData = JsonConvert.SerializeObject(jUserData);

                        addface_result = APIReference.Instance.LargePersonGroupPersonInstance.AddFaceAsync(identifier, personId, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty).Result;

                        if (addface_result != null)
                        {
                            List<DetectResult> detection_result = APIReference.Instance.FaceInstance.DetectAsync(faceAPISettingsFixture.TestImageUrl, "age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise", true, true).Result;

                            if (detection_result != null)
                            {
                                result = APIReference.Instance.FaceInstance.VerifyAsync(string.Empty, string.Empty, detection_result[0].faceId, string.Empty, identifier, personId).Result;
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
                    var deletion_person_result = APIReference.Instance.LargePersonGroupPersonInstance.DeleteAsync(identifier, personId).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_person_result}");

                    var deletion_group_result = APIReference.Instance.LargePersonGroupInstance.DeleteAsync(identifier).Result;
                    System.Diagnostics.Trace.Write($"Deletion Result: {deletion_group_result}");
                }

                Assert.True(result != null);
            });
        }
    }
}