using FaceClientSDK.Domain.LargePersonGroupPerson;
using FaceClientSDK.Tests.Fixtures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
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
        }

        [Fact]
        public void AddFaceAsyncTest()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void CreateAsyncTest()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void DeleteAsyncTest()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void DeleteFaceAsyncTest()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void GetAsyncTest()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void GetFaceAsyncTest()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ListAsyncTest()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void UpdateAsyncTest()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void UpdateFaceAsyncTest()
        {
            throw new NotImplementedException();
        }
    }
}