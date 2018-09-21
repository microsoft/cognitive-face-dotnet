using FaceClientSDK.Domain.Face;
using FaceClientSDK.Domain.FaceList;
using FaceClientSDK.Tests.Fixtures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            var timeout = 10;
            var task = Task.Run(()=> {

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
            var wait = Task.WaitAll(new[] { task }, TimeSpan.FromSeconds(timeout));

            if (task.Exception != null)
            {
                if (task.Exception.InnerExceptions.Count == 1)
                {
                    throw task.Exception.InnerExceptions[0];
                }

                throw task.Exception;
            }

            if (!wait)
            {
                throw new TimeoutException($"Not completed.");
            }
        }

        [Fact]
        public void FindSimilarAsyncTest()
        {
            var timeout = 10;
            var task = Task.Run(() => {

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
            var wait = Task.WaitAll(new[] { task }, TimeSpan.FromSeconds(timeout));

            if (task.Exception != null)
            {
                if (task.Exception.InnerExceptions.Count == 1)
                {
                    throw task.Exception.InnerExceptions[0];
                }

                throw task.Exception;
            }

            if (!wait)
            {
                throw new TimeoutException($"Not completed.");
            }
        }
    }
}