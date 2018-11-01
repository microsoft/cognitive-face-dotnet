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
        public string TestGroupImageUrl { get; set; }

        public FaceAPISettingsFixture()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddIniFile("config.ini", optional: false, reloadOnChange: true)
                .Build();

            FaceAPIKey = config.GetSection("general:FaceAPIKey").Value;
            FaceAPIZone = config.GetSection("general:FaceAPIZone").Value;
            TestImageUrl = config.GetSection("general:TestImageUrl").Value;
            TestGroupImageUrl = config.GetSection("general:TestGroupImageUrl").Value;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
        }
    }
}