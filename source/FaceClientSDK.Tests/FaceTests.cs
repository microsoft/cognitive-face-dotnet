using FaceClientSDK.Domain.Face;
using FaceClientSDK.Domain.FaceList;
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

                    AddFaceResult addface_result = null;
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
    }
}