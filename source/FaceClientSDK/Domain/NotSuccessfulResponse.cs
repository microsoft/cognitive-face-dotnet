using Newtonsoft.Json;

namespace FaceClientSDK.Domain
{
    public class NotSuccessfulResponse
    {
        public class NotSuccessfulResponseError
        {
            private string _code;

            public string code
            {
                get
                {
                    return (string.IsNullOrEmpty(_code)) ? statusCode.ToString() : _code;
                }
                set { _code = value; }
            }

            [JsonProperty("statusCode")]
            private int statusCode { get; set; }

            public string message { get; set; }
        }

        public NotSuccessfulResponseError error { get; set; }
    }
}