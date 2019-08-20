using FaceClientSDK.Domain.FaceList;
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
    public class FaceListTests : IDisposable
    {
        public FaceListTests()
        {
            ApiReference.FaceAPIKey = "face_api_key";
            ApiReference.FaceAPIZone = "face_api_zone";
        }

        public void Dispose()
        {
        }

        [Fact]
        public async void AddFaceAsyncTest()
        {
            AddFaceResult objresult = new AddFaceResult();
            objresult.persistedFaceId = Guid.NewGuid().ToString();
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
            var result = await ApiReference.Instance.FaceList.AddFaceAsync("faceListId", "url", "userData", string.Empty);

            Assert.Equal(objresult.persistedFaceId, result.persistedFaceId);
        }

        [Fact]
        public async void CreateAsyncTest()
        {
            bool objresult = true;
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
            var result = await ApiReference.Instance.FaceList.CreateAsync("faceListId", "name", "userData");

            Assert.Equal(objresult, result);
        }

        [Fact]
        public async void DeleteAsyncTest()
        {
            bool objresult = true;
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
            var result = await ApiReference.Instance.FaceList.DeleteAsync("faceListId");

            Assert.Equal(objresult, result);
        }

        [Fact]
        public async void DeleteFaceAsyncTest()
        {
            bool objresult = true;
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
            var result = await ApiReference.Instance.FaceList.DeleteFaceAsync("faceListId", "persistedFaceId");

            Assert.Equal(objresult, result);
        }

        [Fact]
        public async void GetAsyncTest()
        {
            GetResult objresult = new GetResult();
            objresult.faceListId = Guid.NewGuid().ToString();
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
            var result = await ApiReference.Instance.FaceList.GetAsync("faceListId");

            Assert.Equal(objresult.faceListId, result.faceListId);
        }

        [Fact]
        public async void ListAsyncTest()
        {
            List<ListResult> objresult = new List<ListResult>();
            ListResult listResult = new ListResult();
            listResult.faceListId = Guid.NewGuid().ToString();
            objresult.Add(listResult);
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
            var result = await ApiReference.Instance.FaceList.ListAsync();

            Assert.Equal(objresult[0].faceListId, result[0].faceListId);
        }

        [Fact]
        public async void UpdateAsyncTest()
        {
            bool objresult = true;
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
            var result = await ApiReference.Instance.FaceList.UpdateAsync("faceListId", "name", "userData");

            Assert.Equal(objresult, result);
        }
    }
}