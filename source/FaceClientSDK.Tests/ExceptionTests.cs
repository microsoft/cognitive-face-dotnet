using FaceClientSDK.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace FaceClientSDK.Tests
{
    public class ExceptionTests
    {
        [Fact]
        public void ReadNotSuccessfulResponseWithCodeTest()
        {
            dynamic jNotSuccessfulResponse = new JObject();
            dynamic jNotSuccessfulResponseError = new JObject();
            jNotSuccessfulResponseError.code = "Unspecified";
            jNotSuccessfulResponseError.message = "Access denied due to invalid subscription key. Make sure you are subscribed to an API you are trying to call and provide the right key.";
            jNotSuccessfulResponse.error = jNotSuccessfulResponseError;
            var rException = JsonConvert.SerializeObject(jNotSuccessfulResponse);

            NotSuccessfulResponse exception = JsonConvert.DeserializeObject<NotSuccessfulResponse>(rException);
            Assert.True(exception.error.code == "Unspecified");
        }

        [Fact]
        public void ReadNotSuccessfulResponseWithStatusCodeTest()
        {
            dynamic jNotSuccessfulResponse = new JObject();
            dynamic jNotSuccessfulResponseError = new JObject();
            jNotSuccessfulResponseError.statusCode = 403;
            jNotSuccessfulResponseError.message = "Out of call volume quota. Quota will be replenished in 2 days.";
            jNotSuccessfulResponse.error = jNotSuccessfulResponseError;
            var rException = JsonConvert.SerializeObject(jNotSuccessfulResponse);

            NotSuccessfulResponse exception = JsonConvert.DeserializeObject<NotSuccessfulResponse>(rException);
            Assert.True(exception.error.code == 403.ToString());
        }
    }
}