using FaceClientSDK;
using FaceClientSDK.Domain;
using FaceClientSDK.Domain.FaceList;
using FaceClientSDK.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FacehttpClientSDK
{
    public class FaceList : IFaceList
    {
        private readonly HttpClient httpClient = null;

        public FaceList(HttpClient httpClient = null)
        {
            if (string.IsNullOrEmpty(ApiReference.FaceAPIKey))
                throw new ArgumentException(message: "FaceAPIKey required by: ApiReference.FaceAPIKey");

            if (string.IsNullOrEmpty(ApiReference.FaceAPIZone))
                throw new ArgumentException(message: "FaceAPIZone required by: ApiReference.FaceAPIZone");

            this.httpClient = (httpClient == null) ? new HttpClient() : httpClient;
        }

        public async Task<AddFaceResult> AddFaceAsync(string faceListId, string url, string userData, string targetFace)
        {
            dynamic body = new JObject();
            body.url = url;
            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiReference.FaceAPIKey);
            var response = await httpClient.PostAsync($"https://{ApiReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/facelists/{faceListId}/persistedFaces?userData={userData}&targetFace={targetFace}", queryString);

            AddFaceResult result = null;
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<AddFaceResult>(json);
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                throw new NotSuccessfulException($"{fex.error.code} - {fex.error.message}");
            }

            return result;
        }

        public async Task<bool> CreateAsync(string faceListId, string name, string userData)
        {
            dynamic body = new JObject();
            body.name = name;
            body.userData = userData;
            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiReference.FaceAPIKey);
            var response = await httpClient.PutAsync($"https://{ApiReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/facelists/{faceListId}", queryString);

            bool result = false;
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                throw new NotSuccessfulException($"{fex.error.code} - {fex.error.message}");
            }

            return result;
        }

        public async Task<bool> DeleteAsync(string faceListId)
        {
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiReference.FaceAPIKey);
            var response = await httpClient.DeleteAsync($"https://{ApiReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/facelists/{faceListId}");

            bool result = false;
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                throw new NotSuccessfulException($"{fex.error.code} - {fex.error.message}");
            }

            return result;
        }

        public async Task<bool> DeleteFaceAsync(string faceListId, string persistedFaceId)
        {
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiReference.FaceAPIKey);
            var response = await httpClient.DeleteAsync($"https://{ApiReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/facelists/{faceListId}/persistedfaces/{persistedFaceId}");

            bool result = false;
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                throw new NotSuccessfulException($"{fex.error.code} - {fex.error.message}");
            }

            return result;
        }

        public async Task<GetResult> GetAsync(string faceListId)
        {
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiReference.FaceAPIKey);
            var response = await httpClient.GetAsync($"https://{ApiReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/facelists/{faceListId}");

            GetResult result = null;
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<GetResult>(json);
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                throw new NotSuccessfulException($"{fex.error.code} - {fex.error.message}");
            }

            return result;
        }

        public async Task<List<ListResult>> ListAsync()
        {
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiReference.FaceAPIKey);
            var response = await httpClient.GetAsync($"https://{ApiReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/facelists");

            List<ListResult> result = null;
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<ListResult>>(json);
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                throw new NotSuccessfulException($"{fex.error.code} - {fex.error.message}");
            }

            return result;
        }

        public async Task<bool> UpdateAsync(string faceListId, string name, string userData)
        {
            dynamic body = new JObject();
            body.name = name;
            body.userData = userData;
            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiReference.FaceAPIKey);
            var response = await httpClient.PatchAsync($"https://{ApiReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/facelists/{faceListId}", queryString);

            bool result = false;
            if (response.IsSuccessStatusCode)
            {
                result = true;
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