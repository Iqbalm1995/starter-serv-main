
using starter_serv.Helper;
using System.Diagnostics.CodeAnalysis;

namespace starter_serv.CronJob
{
    public class ApplicationScheduledTaskJob
    {
        private readonly ILogger<ApplicationScheduledTaskJob> _logger;

        public ApplicationScheduledTaskJob()
        {
        }

        [ExcludeFromCodeCoverage]
        public ApplicationScheduledTaskJob(
            ILogger<ApplicationScheduledTaskJob> logger)
        {
            _logger = logger;
        }
        public void ExecuteJob()
        {

            string setMsg = $"Task has been executed - At {DateTimeHelper.GetCurrentDateTime()}";

            Console.WriteLine(setMsg);
            _logger.LogInformation(setMsg);
            // Add your desired logic here
        }
    }
}
