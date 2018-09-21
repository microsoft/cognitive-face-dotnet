using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace FaceClientSDK.Tests.Fixtures
{
    public class FaceAPISettingsFixture : IDisposable
    {
        public string FaceAPIKey { get; set; }
        public string FaceAPIZone { get; set; }
        public string TestImageUrl { get; set; }
        public int Timeout { get; set; }

        public FaceAPISettingsFixture()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddIniFile("config.ini", optional: false, reloadOnChange: true)
                .Build();

            FaceAPIKey = config.GetSection("general:FaceAPIKey").Value;
            FaceAPIZone = config.GetSection("general:FaceAPIZone").Value;
            TestImageUrl = config.GetSection("general:TestImageUrl").Value;
            Timeout = Convert.ToInt32(config.GetSection("general:Timeout").Value);
        }

        public void Dispose()
        {
        }
    }
}