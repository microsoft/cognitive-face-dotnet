using FaceClientSDK.Domain;
using FaceClientSDK.Domain.Face;
using FaceClientSDK.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FaceClientSDK
{
    public class Face : IFace
    {
        private readonly HttpClient httpClient = null;

        public Face(HttpClient httpClient = null)
        {
            if (string.IsNullOrEmpty(ApiReference.FaceAPIKey))
                throw new ArgumentException(message: "FaceAPIKey required by: ApiReference.FaceAPIKey");

            if (string.IsNullOrEmpty(ApiReference.FaceAPIZone))
                throw new ArgumentException(message: "FaceAPIZone required by: ApiReference.FaceAPIZone");

            this.httpClient = (httpClient == null) ? new HttpClient() : httpClient;
        }

        public async Task<List<DetectResult>> DetectAsync(string url, string returnFaceAttributes, bool returnFaceId = false, bool returnFaceLandmarks = false)
        {
            dynamic body = new JObject();
            body.url = url;
            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiReference.FaceAPIKey);
            var response = await httpClient.PostAsync($"https://{ApiReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/detect?returnFaceId={returnFaceId}&returnFaceLandmarks={returnFaceLandmarks}&returnFaceAttributes={returnFaceAttributes}", queryString);

            List<DetectResult> result = null;
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<DetectResult>>(json);
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                throw new NotSuccessfulException($"{fex.error.code} - {fex.error.message}");
            }

            return result;
        }

        public async Task<List<FindSimilarResult>> FindSimilarAsync(string faceId, string faceListId, string largeFaceListId, string[] faceIds, int maxNumOfCandidatesReturned, string mode)
        {
            dynamic body = new JObject();
            body.faceId = faceId;

            if (faceListId != string.Empty)
                body.faceListId = faceListId;

            if (largeFaceListId != string.Empty)
                body.largeFaceListId = largeFaceListId;

            if (faceIds.Length > 0)
                body.faceIds = JArray.FromObject(faceIds);

            body.maxNumOfCandidatesReturned = maxNumOfCandidatesReturned;
            body.mode = mode;
            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiReference.FaceAPIKey);
            var response = await httpClient.PostAsync($"https://{ApiReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/findsimilars", queryString);

            List<FindSimilarResult> result = null;
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<FindSimilarResult>>(json);
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                throw new NotSuccessfulException($"{fex.error.code} - {fex.error.message}");
            }

            return result;
        }

        public async Task<VerifyResult> VerifyAsync(string faceId1, string faceId2, string faceId, string personGroupId, string largePersonGroupId, string personId)
        {
            dynamic body = new JObject();

            if (faceId1 != string.Empty)
                body.faceId1 = faceId1;

            if (faceId2 != string.Empty)
                body.faceId2 = faceId2;

            if (faceId != string.Empty)
                body.faceId = faceId;

            if (personGroupId != string.Empty)
                body.personGroupId = personGroupId;

            if (largePersonGroupId != string.Empty)
                body.largePersonGroupId = largePersonGroupId;

            if (personId != string.Empty)
                body.personId = personId;

            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiReference.FaceAPIKey);
            var response = await httpClient.PostAsync($"https://{ApiReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/verify", queryString);

            VerifyResult result = null;
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<VerifyResult>(json);
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                throw new NotSuccessfulException($"{fex.error.code} - {fex.error.message}");
            }

            return result;
        }

        public async Task<GroupResult> GroupAsync(string[] faceIds)
        {
            dynamic body = new JObject();

            if (faceIds.Length > 0)
                body.faceIds = JArray.FromObject(faceIds);

            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiReference.FaceAPIKey);
            var response = await httpClient.PostAsync($"https://{ApiReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/group", queryString);

            GroupResult result = null;
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<GroupResult>(json);
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                throw new NotSuccessfulException($"{fex.error.code} - {fex.error.message}");
            }

            return result;
        }

        public async Task<List<IdentifyResult>> IdentifyAsync(string largePersonGroupId, string[] faceIds, int maxNumOfCandidatesReturned, double confidenceThreshold)
        {
            dynamic body = new JObject();

            if (largePersonGroupId != string.Empty)
                body.largePersonGroupId = largePersonGroupId;

            if (faceIds.Length > 0)
                body.faceIds = JArray.FromObject(faceIds);

            body.maxNumOfCandidatesReturned = maxNumOfCandidatesReturned;
            body.confidenceThreshold = confidenceThreshold;

            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiReference.FaceAPIKey);
            var response = await httpClient.PostAsync($"https://{ApiReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/identify", queryString);

            List<IdentifyResult> result = null;
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<IdentifyResult>>(json);
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                throw new NotSuccessfulException($"{fex.error.code} - {fex.error.message}");
            }

            return result;
        }
    }
}