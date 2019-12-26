using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace SthlmTechEvents.App
{
    public static class FetchEvents
    {
        [FunctionName("FetchEvents")]
        public static async Task RunAsync([TimerTrigger("0 */5 * ? * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Fetch events function executed at: {DateTime.UtcNow}");
        }
    }
}