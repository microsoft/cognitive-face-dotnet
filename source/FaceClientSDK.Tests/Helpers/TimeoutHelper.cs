using System;
using System.Threading.Tasks;

namespace FaceClientSDK.Tests.Helpers
{
    public class TimeoutHelper
    {
        public static int Timeout { get; set; }

        public static void ThrowExceptionInTimeout(Action action)
        {
            var timeout = Timeout;
            var task = Task.Run(action);
            var arr = new[] { task };
            var wait = Task.WaitAll(arr, TimeSpan.FromSeconds(timeout));

            if (task.Exception != null)
            {
                if (task.Exception.InnerExceptions.Count > 0)
                {
                    throw task.Exception.InnerExceptions[0];
                }
                throw task.Exception;
            }

            if (!wait)
            {
                throw new TimeoutException($"There was a timeout exception.");
            }
        }
    }
}