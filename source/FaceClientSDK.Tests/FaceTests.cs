using FaceClientSDK.Domain.Face;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FaceClientSDK.Tests
{
    public class FaceTests : IDisposable
    {
        public FaceTests()
        {
            ApiReference.FaceAPIKey = "face_api_key";
            ApiReference.FaceAPIZone = "face_api_zone";
        }

        public void Dispose()
        {
        }

        [Fact]
        public async void DetectAsyncTest()
        {
            List<DetectResult> objresult = new List<DetectResult>();
            DetectResult detectResult = new DetectResult();
            detectResult.faceId = Guid.NewGuid().ToString();
            objresult.Add(detectResult);
            var jsonResult = JsonConvert.SerializeObject(objresult);

            var handlerMock = new Mock<HttpClientHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonResult),
                })

                .Verifiable();

            ApiReference.HttpClient = new HttpClient(handlerMock.Object);
            var result = await ApiReference.Instance.Face.DetectAsync("url", "age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise", true, true);

            Assert.Equal(detectResult.faceId, result[0].faceId);
        }

        [Fact]
        public async void FindSimilarAsyncTest()
        {
            List<FindSimilarResult> objresult = new List<FindSimilarResult>();
            FindSimilarResult findSimilarResult = new FindSimilarResult();
            findSimilarResult.faceId = System.Guid.NewGuid().ToString();
            objresult.Add(findSimilarResult);
            var jsonResult = JsonConvert.SerializeObject(objresult);

            var handlerMock = new Mock<HttpClientHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonResult),
                })

                .Verifiable();

            ApiReference.HttpClient = new HttpClient(handlerMock.Object);
            var result = await ApiReference.Instance.Face.FindSimilarAsync("faceId", "faceListId", string.Empty, new string[] { }, 10, "matchPerson");

            Assert.Equal(findSimilarResult.faceId, result[0].faceId);
        }

        [Fact]
        public async void VerifyAsyncTest()
        {
            VerifyResult objresult = new VerifyResult();
            objresult.isIdentical = true;
            var jsonResult = JsonConvert.SerializeObject(objresult);

            var handlerMock = new Mock<HttpClientHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonResult),
                })

                .Verifiable();

            ApiReference.HttpClient = new HttpClient(handlerMock.Object);
            var result = await ApiReference.Instance.Face.VerifyAsync(string.Empty, string.Empty, "faceId", string.Empty, "largePersonGroupId", "personId");

            Assert.Equal(objresult.isIdentical, result.isIdentical);
        }

        [Fact]
        public async void GroupAsyncTest()
        {
            GroupResult objresult = new GroupResult();
            objresult.groups = new List<List<string>>();
            List<string> l1 = new List<string>();
            l1.Add("group");
            objresult.groups.Add(l1);
            var jsonResult = JsonConvert.SerializeObject(objresult);

            var handlerMock = new Mock<HttpClientHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonResult),
                })

                .Verifiable();

            ApiReference.HttpClient = new HttpClient(handlerMock.Object);
            var groupResult = await ApiReference.Instance.Face.GroupAsync(new string[] { });

            Assert.Equal("group", groupResult.groups[0][0]);
        }

        [Fact]
        public async void IdentifyAsyncTest()
        {
            List<IdentifyResult> objresult = new List<IdentifyResult>();
            IdentifyResult identifyResult = new IdentifyResult();
            identifyResult.faceId = Guid.NewGuid().ToString();
            objresult.Add(identifyResult);
            var jsonResult = JsonConvert.SerializeObject(objresult);

            var handlerMock = new Mock<HttpClientHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonResult),
                })

                .Verifiable();

            ApiReference.HttpClient = new HttpClient(handlerMock.Object);
            var result = await ApiReference.Instance.Face.IdentifyAsync("largePersonGroupId", new string[] { }, 1, 0);

            Assert.Equal(identifyResult.faceId, result[0].faceId);
        }
    }
}