using FaceClientSDK.Domain.PersonGroupPerson;
using FaceClientSDK.Tests.Fixtures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Xunit;

namespace FaceClientSDK.Tests
{
    public class PersonGroupPersonTests : IClassFixture<FaceAPISettingsFixture>
    {
        private FaceAPISettingsFixture faceAPISettingsFixture = null;

        public PersonGroupPersonTests(FaceAPISettingsFixture fixture)
        {
            faceAPISettingsFixture = fixture;

            APIReference.FaceAPIKey = faceAPISettingsFixture.FaceAPIKey;
            APIReference.FaceAPIZone = faceAPISettingsFixture.FaceAPIZone;
        }

        [Fact]
        public async void AddFaceAsyncTest()
        {
            AddFaceResult result = null;
            var identifier = System.Guid.NewGuid().ToString();
            var personId = string.Empty;

            try
            {
                var creation_group_result = await APIReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);

                var creation_person_result = await APIReference.Instance.PersonGroupPerson.CreateAsync(identifier, identifier, identifier);
                personId = creation_person_result.personId;

                if (creation_group_result)
                {
                    dynamic jUserData = new JObject();
                    jUserData.UserDataSample = "User Data Sample";
                    var rUserData = JsonConvert.SerializeObject(jUserData);

                    result = await APIReference.Instance.PersonGroupPerson.AddFaceAsync(identifier, creation_person_result.personId, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_person_result = await APIReference.Instance.PersonGroupPerson.DeleteAsync(identifier, personId);
                var deletion_group_result = await APIReference.Instance.PersonGroup.DeleteAsync(identifier);
            }

            Assert.True(result != null);
        }

        [Fact]
        public async void CreateAsyncTest()
        {
            CreateResult result = null;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_group_result = await APIReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);

                if (creation_group_result)
                    result = await APIReference.Instance.PersonGroupPerson.CreateAsync(identifier, identifier, identifier);
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_person_result = await APIReference.Instance.PersonGroupPerson.DeleteAsync(identifier, result.personId);
                var deletion_group_result = await APIReference.Instance.PersonGroup.DeleteAsync(identifier);
            }

            Assert.True(result != null);
        }

        [Fact]
        public async void DeleteAsyncTest()
        {
            bool result = false;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_group_result = await APIReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);

                var creation_person_result = await APIReference.Instance.PersonGroupPerson.CreateAsync(identifier, identifier, identifier);

                result = await APIReference.Instance.PersonGroupPerson.DeleteAsync(identifier, creation_person_result.personId);

                var deletion_group_result = await APIReference.Instance.PersonGroup.DeleteAsync(identifier);
            }
            catch
            {
                throw;
            }

            Assert.True(result);
        }

        [Fact]
        public async void DeleteFaceAsyncTest()
        {
            bool result = false;
            var identifier = System.Guid.NewGuid().ToString();
            var personId = string.Empty;

            try
            {
                var creation_group_result = await APIReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);

                var creation_person_result = await APIReference.Instance.PersonGroupPerson.CreateAsync(identifier, identifier, identifier);
                personId = creation_person_result.personId;

                AddFaceResult addface_result = null;
                if (creation_group_result)
                {
                    dynamic jUserData = new JObject();
                    jUserData.UserDataSample = "User Data Sample";
                    var rUserData = JsonConvert.SerializeObject(jUserData);

                    addface_result = await APIReference.Instance.PersonGroupPerson.AddFaceAsync(identifier, personId, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty);

                    if (addface_result != null)
                        result = await APIReference.Instance.PersonGroupPerson.DeleteFaceAsync(identifier, personId, addface_result.persistedFaceId);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_person_result = await APIReference.Instance.PersonGroupPerson.DeleteAsync(identifier, personId);
                var deletion_group_result = await APIReference.Instance.PersonGroup.DeleteAsync(identifier);
            }

            Assert.True(result);
        }

        [Fact]
        public async void GetAsyncTest()
        {
            GetResult result = null;
            var identifier = System.Guid.NewGuid().ToString();
            var personId = string.Empty;

            try
            {
                var creation_group_result = await APIReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);

                var creation_person_result = await APIReference.Instance.PersonGroupPerson.CreateAsync(identifier, identifier, identifier);
                personId = creation_person_result.personId;

                if (creation_group_result)
                    result = await APIReference.Instance.PersonGroupPerson.GetAsync(identifier, personId);
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_person_result = await APIReference.Instance.PersonGroupPerson.DeleteAsync(identifier, personId);
                var deletion_group_result = await APIReference.Instance.PersonGroup.DeleteAsync(identifier);
            }

            Assert.True(result != null);
        }

        [Fact]
        public async void GetFaceAsyncTest()
        {
            GetFaceResult result = null;
            var identifier = System.Guid.NewGuid().ToString();
            var personId = string.Empty;

            try
            {
                var creation_group_result = await APIReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);

                var creation_person_result = await APIReference.Instance.PersonGroupPerson.CreateAsync(identifier, identifier, identifier);
                personId = creation_person_result.personId;

                AddFaceResult addface_result = null;
                if (creation_group_result)
                {
                    dynamic jUserData = new JObject();
                    jUserData.UserDataSample = "User Data Sample";
                    var rUserData = JsonConvert.SerializeObject(jUserData);

                    addface_result = await APIReference.Instance.PersonGroupPerson.AddFaceAsync(identifier, personId, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty);

                    if (addface_result != null)
                        result = await APIReference.Instance.PersonGroupPerson.GetFaceAsync(identifier, personId, addface_result.persistedFaceId);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_person_result = await APIReference.Instance.PersonGroupPerson.DeleteAsync(identifier, personId);
                var deletion_group_result = await APIReference.Instance.PersonGroup.DeleteAsync(identifier);
            }

            Assert.True(result != null);
        }

        [Fact]
        public async void ListAsyncTest()
        {
            List<ListResult> result = null;
            var identifier = System.Guid.NewGuid().ToString();
            var personId = string.Empty;

            try
            {
                var creation_group_result = await APIReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);

                var creation_person_result = await APIReference.Instance.PersonGroupPerson.CreateAsync(identifier, identifier, identifier);
                personId = creation_person_result.personId;

                if (creation_group_result)
                    result = await APIReference.Instance.PersonGroupPerson.ListAsync(identifier, string.Empty, 1000);
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_person_result = await APIReference.Instance.PersonGroupPerson.DeleteAsync(identifier, personId);
                var deletion_group_result = await APIReference.Instance.PersonGroup.DeleteAsync(identifier);
            }

            Assert.True(result != null);
        }

        [Fact]
        public async void UpdateAsyncTest()
        {
            bool result = false;
            var identifier = System.Guid.NewGuid().ToString();
            var personId = string.Empty;

            try
            {
                var creation_group_result = await APIReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);

                var creation_person_result = await APIReference.Instance.PersonGroupPerson.CreateAsync(identifier, identifier, identifier);
                personId = creation_person_result.personId;

                if (creation_group_result)
                    result = await APIReference.Instance.PersonGroupPerson.UpdateAsync(identifier, personId, "Name", "User Data Sample");
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_person_result = await APIReference.Instance.PersonGroupPerson.DeleteAsync(identifier, personId);
                var deletion_group_result = await APIReference.Instance.PersonGroup.DeleteAsync(identifier);
            }

            Assert.True(result);
        }

        [Fact]
        public async void UpdateFaceAsyncTest()
        {
            bool result = false;
            var identifier = System.Guid.NewGuid().ToString();
            var personId = string.Empty;

            try
            {
                var creation_group_result = await APIReference.Instance.PersonGroup.CreateAsync(identifier, identifier, identifier);

                var creation_person_result = await APIReference.Instance.PersonGroupPerson.CreateAsync(identifier, identifier, identifier);
                personId = creation_person_result.personId;

                AddFaceResult addface_result = null;
                if (creation_group_result)
                {
                    dynamic jUserData = new JObject();
                    jUserData.UserDataSample = "User Data Sample";
                    var rUserData = JsonConvert.SerializeObject(jUserData);

                    addface_result = await APIReference.Instance.PersonGroupPerson.AddFaceAsync(identifier, personId, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty);

                    if (addface_result != null)
                        result = await APIReference.Instance.PersonGroupPerson.UpdateFaceAsync(identifier, personId, addface_result.persistedFaceId, "User Data Sample");
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_person_result = await APIReference.Instance.PersonGroupPerson.DeleteAsync(identifier, personId);
                var deletion_group_result = await APIReference.Instance.PersonGroup.DeleteAsync(identifier);
            }

            Assert.True(result);
        }
    }
}