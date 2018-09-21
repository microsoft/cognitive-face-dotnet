namespace FaceClientSDK.Domain.LargeFaceList
{
    public class GetTrainingStatusResult
    {
        public string status { get; set; }
        public string createdDateTime { get; set; }
        public string lastActionDateTime { get; set; }
        public string lastSuccessfulTrainingDateTime { get; set; }
        public string message { get; set; }
    }
}