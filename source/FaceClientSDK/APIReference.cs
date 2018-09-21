using FaceClientSDK.Interfaces;
using FaceClientSDK.Domain;
using DomainFace = FaceClientSDK.Domain.Face;
using DomainFaceList = FaceClientSDK.Domain.FaceList;
using DomainLargeFaceList = FaceClientSDK.Domain.LargeFaceList;
using DomainLargePersonGroup = FaceClientSDK.Domain.LargePersonGroup;
using DomainLargePersonGroupPerson = FaceClientSDK.Domain.LargePersonGroupPerson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FaceClientSDK
{
    public class APIReference
    {
        private static APIReference instance = null;
        private static readonly object padlock = new object();

        public static string FaceAPIKey { get; set; }
        public static string FaceAPIZone { get; set; }
        public Face FaceInstance { get; set; } = Face.Instance;
        public FaceList FaceListInstance { get; set; } = FaceList.Instance;
        public LargeFaceList LargeFaceListInstance { get; set; } = LargeFaceList.Instance;
        public LargePersonGroup LargePersonGroupInstance { get; set; } = LargePersonGroup.Instance;
        public LargePersonGroupPerson LargePersonGroupPersonInstance { get; set; } = LargePersonGroupPerson.Instance;

        APIReference()
        {
        }

        public static APIReference Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new APIReference();
                    }
                    return instance;
                }
            }
        }
    }

    public class Face : IFace
    {
        private static Face instance = null;
        private static readonly object padlock = new object();

        Face()
        {
            if (string.IsNullOrEmpty(APIReference.FaceAPIKey))
                throw new Exception("FaceAPIKey required by: APIReference.FaceAPIKey");

            if (string.IsNullOrEmpty(APIReference.FaceAPIZone))
                throw new Exception("FaceAPIZone required by: APIReference.FaceAPIZone");
        }

        public static Face Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Face();
                    }
                    return instance;
                }
            }
        }

        public async Task<List<DomainFace.DetectResult>> DetectAsync(string url, string returnFaceAttributes, bool returnFaceId = false, bool returnFaceLandmarks = false)
        {
            dynamic body = new JObject();
            body.url = url;
            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.PostAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/detect?returnFaceId={returnFaceId}&returnFaceLandmarks={returnFaceLandmarks}&returnFaceAttributes={returnFaceAttributes}", queryString);

                List<DomainFace.DetectResult> result = null;
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<DomainFace.DetectResult>>(json);
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<List<DomainFace.FindSimilarResult>> FindSimilarAsync(string faceId, string faceListId, string largeFaceListId, string[] faceIds, int maxNumOfCandidatesReturned, string mode)
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

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.PostAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/findsimilars", queryString);

                List<DomainFace.FindSimilarResult> result = null;
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<DomainFace.FindSimilarResult>>(json);
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }
    }

    public class FaceList : IFaceList
    {
        private static FaceList instance = null;
        private static readonly object padlock = new object();

        FaceList()
        {
            if (string.IsNullOrEmpty(APIReference.FaceAPIKey))
                throw new Exception("FaceAPIKey required by: APIReference.FaceAPIKey");

            if (string.IsNullOrEmpty(APIReference.FaceAPIZone))
                throw new Exception("FaceAPIZone required by: APIReference.FaceAPIZone");
        }

        public static FaceList Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new FaceList();
                    }
                    return instance;
                }
            }
        }

        public async Task<DomainFaceList.AddFaceResult> AddFaceAsync(string faceListId, string url, string userData, string targetFace)
        {
            dynamic body = new JObject();
            body.url = url;
            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.PostAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/facelists/{faceListId}/persistedFaces?userData={userData}&targetFace={targetFace}", queryString);

                DomainFaceList.AddFaceResult result = null;
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<DomainFaceList.AddFaceResult>(json);
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<bool> CreateAsync(string faceListId, string name, string userData)
        {
            dynamic body = new JObject();
            body.name = name;
            body.userData = userData;
            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.PutAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/facelists/{faceListId}", queryString);

                bool result = false;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }
                return result;
            }
        }

        public async Task<bool> DeleteAsync(string faceListId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.DeleteAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/facelists/{faceListId}");

                bool result = false;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }
                return result;
            }
        }

        public async Task<bool> DeleteFaceAsync(string faceListId, string persistedFaceId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.DeleteAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/facelists/{faceListId}/persistedfaces/{persistedFaceId}");

                bool result = false;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<DomainFaceList.GetResult> GetAsync(string faceListId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.GetAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/facelists/{faceListId}");

                DomainFaceList.GetResult result = null;
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<DomainFaceList.GetResult>(json);
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<List<DomainFaceList.ListResult>> ListAsync()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.GetAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/facelists");

                List<DomainFaceList.ListResult> result = null;
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<DomainFaceList.ListResult>>(json);
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<bool> UpdateAsync(string faceListId, string name, string userData)
        {
            dynamic body = new JObject();
            body.name = name;
            body.userData = userData;
            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.PatchAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/facelists/{faceListId}", queryString);

                bool result = false;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }
    }

    public class LargeFaceList : ILargeFaceList
    {
        private static LargeFaceList instance = null;
        private static readonly object padlock = new object();

        LargeFaceList()
        {
            if (string.IsNullOrEmpty(APIReference.FaceAPIKey))
                throw new Exception("FaceAPIKey required by: APIReference.FaceAPIKey");

            if (string.IsNullOrEmpty(APIReference.FaceAPIZone))
                throw new Exception("FaceAPIZone required by: APIReference.FaceAPIZone");
        }

        public static LargeFaceList Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new LargeFaceList();
                    }
                    return instance;
                }
            }
        }

        public async Task<DomainLargeFaceList.AddFaceResult> AddFaceAsync(string largeFaceListId, string url, string userData, string targetFace)
        {
            dynamic body = new JObject();
            body.url = url;
            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.PostAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largefacelists/{largeFaceListId}/persistedFaces?userData={userData}&targetFace={targetFace}", queryString);

                DomainLargeFaceList.AddFaceResult result = null;
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<DomainLargeFaceList.AddFaceResult>(json);
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<bool> CreateAsync(string largeFaceListId, string name, string userData)
        {
            dynamic body = new JObject();
            body.name = name;
            body.userData = userData;
            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.PutAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largefacelists/{largeFaceListId}", queryString);

                bool result = false;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }
                return result;
            }
        }

        public async Task<bool> DeleteAsync(string largeFaceListId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.DeleteAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largefacelists/{largeFaceListId}");

                bool result = false;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }
                return result;
            }
        }

        public async Task<bool> DeleteFaceAsync(string largeFaceListId, string persistedFaceId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.DeleteAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largefacelists/{largeFaceListId}/persistedfaces/{persistedFaceId}");

                bool result = false;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<DomainLargeFaceList.GetResult> GetAsync(string largeFaceListId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.GetAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largefacelists/{largeFaceListId}");

                DomainLargeFaceList.GetResult result = null;
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<DomainLargeFaceList.GetResult>(json);
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<DomainLargeFaceList.GetFaceResult> GetFaceAsync(string largeFaceListId, string persistedFaceId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.GetAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largefacelists/{largeFaceListId}/persistedfaces/{persistedFaceId}");

                DomainLargeFaceList.GetFaceResult result = null;
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<DomainLargeFaceList.GetFaceResult>(json);
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<DomainLargeFaceList.GetTrainingStatusResult> GetTrainingStatusAsync(string largeFaceListId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.GetAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largefacelists/{largeFaceListId}/training");

                DomainLargeFaceList.GetTrainingStatusResult result = null;
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<DomainLargeFaceList.GetTrainingStatusResult>(json);
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<List<DomainLargeFaceList.ListResult>> ListAsync(string start, int top)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.GetAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largefacelists?start={start}&top={top}");

                List<DomainLargeFaceList.ListResult> result = null;
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<DomainLargeFaceList.ListResult>>(json);
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<List<DomainLargeFaceList.ListFaceResult>> ListFaceAsync(string largeFaceListId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.GetAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largefacelists/{largeFaceListId}/persistedfaces");

                List<DomainLargeFaceList.ListFaceResult> result = null;
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<DomainLargeFaceList.ListFaceResult>>(json);
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<bool> TrainAsync(string largeFaceListId)
        {
            dynamic body = new JObject();
            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.PostAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largefacelists/{largeFaceListId}/train", queryString);

                bool result = false;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<bool> UpdateAsync(string largeFaceListId, string name, string userData)
        {
            dynamic body = new JObject();
            body.name = name;
            body.userData = userData;
            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.PatchAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largefacelists/{largeFaceListId}", queryString);

                bool result = false;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<bool> UpdateFaceAsync(string largeFaceListId, string persistedFaceId, string userData)
        {
            dynamic body = new JObject();
            body.userData = userData;
            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.PatchAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largefacelists/{largeFaceListId}/persistedfaces/{persistedFaceId}", queryString);

                bool result = false;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }
    }

    public class LargePersonGroup : ILargePersonGroup
    {
        private static LargePersonGroup instance = null;
        private static readonly object padlock = new object();

        LargePersonGroup()
        {
            if (string.IsNullOrEmpty(APIReference.FaceAPIKey))
                throw new Exception("FaceAPIKey required by: APIReference.FaceAPIKey");

            if (string.IsNullOrEmpty(APIReference.FaceAPIZone))
                throw new Exception("FaceAPIZone required by: APIReference.FaceAPIZone");
        }

        public static LargePersonGroup Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new LargePersonGroup();
                    }
                    return instance;
                }
            }
        }

        public async Task<bool> CreateAsync(string largePersonGroupId, string name, string userData)
        {
            dynamic body = new JObject();
            body.name = name;
            body.userData = userData;
            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.PutAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largepersongroups/{largePersonGroupId}", queryString);

                bool result = false;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }
                return result;
            }
        }

        public async Task<bool> DeleteAsync(string largePersonGroupId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.DeleteAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largepersongroups/{largePersonGroupId}");

                bool result = false;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }
                return result;
            }
        }

        public async Task<DomainLargePersonGroup.GetResult> GetAsync(string largePersonGroupId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.GetAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largepersongroups/{largePersonGroupId}");

                DomainLargePersonGroup.GetResult result = null;
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<DomainLargePersonGroup.GetResult>(json);
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<DomainLargePersonGroup.GetTrainingStatusResult> GetTrainingStatusAsync(string largePersonGroupId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.GetAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largepersongroups/{largePersonGroupId}/training");

                DomainLargePersonGroup.GetTrainingStatusResult result = null;
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<DomainLargePersonGroup.GetTrainingStatusResult>(json);
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<List<DomainLargePersonGroup.ListResult>> ListAsync(string start, int top)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.GetAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largepersongroups?start={start}&top={top}");

                List<DomainLargePersonGroup.ListResult> result = null;
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<DomainLargePersonGroup.ListResult>>(json);
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<bool> TrainAsync(string largePersonGroupId)
        {
            dynamic body = new JObject();
            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.PostAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largepersongroups/{largePersonGroupId}/train", queryString);

                bool result = false;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<bool> UpdateAsync(string largePersonGroupId, string name, string userData)
        {
            dynamic body = new JObject();
            body.name = name;
            body.userData = userData;
            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.PatchAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largepersongroups/{largePersonGroupId}", queryString);

                bool result = false;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }
    }

    public class LargePersonGroupPerson : ILargePersonGroupPerson
    {
        private static LargePersonGroupPerson instance = null;
        private static readonly object padlock = new object();

        LargePersonGroupPerson()
        {
            if (string.IsNullOrEmpty(APIReference.FaceAPIKey))
                throw new Exception("FaceAPIKey required by: APIReference.FaceAPIKey");

            if (string.IsNullOrEmpty(APIReference.FaceAPIZone))
                throw new Exception("FaceAPIZone required by: APIReference.FaceAPIZone");
        }

        public static LargePersonGroupPerson Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new LargePersonGroupPerson();
                    }
                    return instance;
                }
            }
        }

        public async Task<DomainLargePersonGroupPerson.AddFaceResult> AddFaceAsync(string largePersonGroupId, string personId, string url, string userData, string targetFace)
        {
            dynamic body = new JObject();
            body.url = url;
            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.PostAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largepersongroups/{largePersonGroupId}/persons/{personId}/persistedfaces?userData={userData}&targetFace={targetFace}", queryString);

                DomainLargePersonGroupPerson.AddFaceResult result = null;
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<DomainLargePersonGroupPerson.AddFaceResult>(json);
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<bool> CreateAsync(string largePersonGroupId, string name, string userData)
        {
            dynamic body = new JObject();
            body.name = name;
            body.userData = userData;
            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.PutAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largepersongroups/{largePersonGroupId}/persons", queryString);

                bool result = false;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }
                return result;
            }
        }

        public async Task<bool> DeleteAsync(string largePersonGroupId, string personId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.DeleteAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largepersongroups/{largePersonGroupId}/persons/{personId}");

                bool result = false;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }
                return result;
            }
        }

        public async Task<bool> DeleteFaceAsync(string largePersonGroupId, string personId, string persistedFaceId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.DeleteAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largepersongroups/{largePersonGroupId}/persons/{personId}/persistedfaces/{persistedFaceId}");

                bool result = false;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<DomainLargePersonGroupPerson.GetResult> GetAsync(string largePersonGroupId, string personId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.GetAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largepersongroups/{largePersonGroupId}/persons/{personId}");

                DomainLargePersonGroupPerson.GetResult result = null;
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<DomainLargePersonGroupPerson.GetResult>(json);
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<DomainLargePersonGroupPerson.GetFaceResult> GetFaceAsync(string largePersonGroupId, string personId, string persistedFaceId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.GetAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largepersongroups/{largePersonGroupId}/persons/{personId}/persistedfaces/{persistedFaceId}");

                DomainLargePersonGroupPerson.GetFaceResult result = null;
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<DomainLargePersonGroupPerson.GetFaceResult>(json);
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<List<DomainLargePersonGroupPerson.ListResult>> ListAsync(string largePersonGroupId, string start, int top)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.GetAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largepersongroups/{largePersonGroupId}/persons?start={start}&top={top}");

                List<DomainLargePersonGroupPerson.ListResult> result = null;
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<DomainLargePersonGroupPerson.ListResult>>(json);
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<bool> UpdateAsync(string largePersonGroupId, string personId, string name, string userData)
        {
            dynamic body = new JObject();
            body.name = name;
            body.userData = userData;
            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.PatchAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largepersongroups/{largePersonGroupId}/persons/{personId}", queryString);

                bool result = false;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }

        public async Task<bool> UpdateFaceAsync(string largePersonGroupId, string personId, string persistedFaceId, string userData)
        {
            dynamic body = new JObject();
            body.userData = userData;
            StringContent queryString = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIReference.FaceAPIKey);
                var response = await client.PatchAsync($"https://{APIReference.FaceAPIZone}.api.cognitive.microsoft.com/face/v1.0/largepersongroups/{largePersonGroupId}/persons/{personId}/persistedfaces/{persistedFaceId}", queryString);

                bool result = false;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    NotSuccessfulResponse fex = JsonConvert.DeserializeObject<NotSuccessfulResponse>(json);
                    throw new Exception($"{fex.error.code} - {fex.error.message}");
                }

                return result;
            }
        }
    }
}