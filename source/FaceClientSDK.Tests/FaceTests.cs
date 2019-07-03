using FaceClientSDK.Domain.Face;
using FaceClientSDK.Tests.Fixtures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using DomainFaceList = FaceClientSDK.Domain.FaceList;
using DomainLargePersonGroupPerson = FaceClientSDK.Domain.LargePersonGroupPerson;

namespace FaceClientSDK.Tests
{
    public class FaceTests : IClassFixture<FaceAPISettingsFixture>
    {
        private FaceAPISettingsFixture faceAPISettingsFixture = null;

        public FaceTests(FaceAPISettingsFixture fixture)
        {
            faceAPISettingsFixture = fixture;

            ApiReference.FaceAPIKey = faceAPISettingsFixture.FaceAPIKey;
            ApiReference.FaceAPIZone = faceAPISettingsFixture.FaceAPIZone;
        }

        [Fact]
        public async void DetectAsyncTest()
        {
            List<DetectResult> result = null;

            try
            {
                result = await ApiReference.Instance.Face.DetectAsync(faceAPISettingsFixture.TestImageUrl, "age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise", true, true);
            }
            catch
            {
                throw;
            }

            Assert.True(result != null);
        }

        [Fact]
        public async void FindSimilarAsyncTest()
        {
            List<FindSimilarResult> result = null;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_result = await ApiReference.Instance.FaceList.CreateAsync(identifier, identifier, identifier);

                DomainFaceList.AddFaceResult addface_result = null;
                if (creation_result)
                {
                    dynamic jUserData = new JObject();
                    jUserData.UserDataSample = "User Data Sample";
                    var rUserData = JsonConvert.SerializeObject(jUserData);

                    addface_result = await ApiReference.Instance.FaceList.AddFaceAsync(identifier, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty);

                    if (addface_result != null)
                    {
                        List<DetectResult> detection_result = await ApiReference.Instance.Face.DetectAsync(faceAPISettingsFixture.TestImageUrl, "age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise", true, true);

                        if (detection_result != null)
                            result = await ApiReference.Instance.Face.FindSimilarAsync(detection_result[0].faceId, identifier, string.Empty, new string[] { }, 10, "matchPerson");
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await ApiReference.Instance.FaceList.DeleteAsync(identifier);
            }

            Assert.True(result != null);
        }

        [Fact]
        public async void VerifyAsyncTest()
        {
            VerifyResult result = null;
            var identifier = System.Guid.NewGuid().ToString();
            var personId = string.Empty;
            try
            {
                var creation_group_result = await ApiReference.Instance.LargePersonGroup.CreateAsync(identifier, identifier, identifier);

                var creation_person_result = await ApiReference.Instance.LargePersonGroupPerson.CreateAsync(identifier, identifier, identifier);
                personId = creation_person_result.personId;

                DomainLargePersonGroupPerson.AddFaceResult addface_result = null;
                if (creation_group_result)
                {
                    dynamic jUserData = new JObject();
                    jUserData.UserDataSample = "User Data Sample";
                    var rUserData = JsonConvert.SerializeObject(jUserData);

                    addface_result = await ApiReference.Instance.LargePersonGroupPerson.AddFaceAsync(identifier, personId, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty);

                    if (addface_result != null)
                    {
                        List<DetectResult> detection_result = await ApiReference.Instance.Face.DetectAsync(faceAPISettingsFixture.TestImageUrl, "age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise", true, true);

                        if (detection_result != null)
                        {
                            result = await ApiReference.Instance.Face.VerifyAsync(string.Empty, string.Empty, detection_result[0].faceId, string.Empty, identifier, personId);
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
                var deletion_person_result = await ApiReference.Instance.LargePersonGroupPerson.DeleteAsync(identifier, personId);
                var deletion_group_result = await ApiReference.Instance.LargePersonGroup.DeleteAsync(identifier);
            }

            Assert.True(result != null);
        }

        [Fact]
        public async void GroupAsyncTest()
        {
            List<DetectResult> detectResult = null;
            GroupResult groupResult = null;

            try
            {
                detectResult = await ApiReference.Instance.Face.DetectAsync(faceAPISettingsFixture.TestGroupImageUrl, "age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise", true, true);
                if (detectResult.Count > 0)
                {
                    groupResult = await ApiReference.Instance.Face.GroupAsync((from result in detectResult select result.faceId).ToArray());
                }
            }
            catch
            {
                throw;
            }

            Assert.True(groupResult != null);
        }

        [Fact]
        public async void IdentifyAsyncTest()
        {
            List<IdentifyResult> result = null;
            var identifier = System.Guid.NewGuid().ToString();
            string[] faceIds = new[] { "" };

            try
            {
                result = await ApiReference.Instance.Face.IdentifyAsync(identifier, faceIds, 1, 0.5);
            }
            catch
            {
                throw;
            }

            Assert.True(result != null);
        }
    }
}