using Cronos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using starter_serv.BusinessProviders;
using starter_serv.Constant;
using starter_serv.Data;
using starter_serv.DataProviders;
using starter_serv.Helper;
using starter_serv.Models;
using starter_serv.ViewModel.Base;
using System.Diagnostics.CodeAnalysis;

namespace starter_serv.CronJob
{
    public class CronJobService : IHostedService, IDisposable
    {
        private readonly ILogger<CronJobService> _logger;
        private readonly CronJobOptions _cronJobOptions;
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;

        [ExcludeFromCodeCoverage]
        public CronJobService(
            ILogger<CronJobService> logger,
            IOptions<CronJobOptions> cronJobOptions,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _cronJobOptions = cronJobOptions.Value;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Create a cron expression for executing every 2 minutes
            //var cronExpression = CronExpression.Parse("*/2 * * * *"); // Runs every 2 minutes
            // Create a cron expression for executing every day at 3 PM GMT+7
            var cronExpression = CronExpression.Parse(_cronJobOptions.CronExpression);

            // Calculate the next occurrence of the cron expression
            var nextOccurrence = cronExpression.GetNextOccurrence(DateTimeOffset.Now, TimeZoneInfo.Local);

            // Calculate the delay until the next occurrence
            var delay = nextOccurrence - DateTimeOffset.Now;

            // Start a timer that will execute the job when the delay elapses
            _timer = new Timer(DoWork, null, (TimeSpan)delay, TimeSpan.FromMilliseconds(-1));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            // Your job logic goes here
            Console.WriteLine("[DoWork] Cron job executed!");
            _logger.LogInformation($"[CRON JOB - DoWork] Cron job executed! Start At : {DateTimeHelper.GetCurrentDateTime()}");

            // Schedule the next occurrence of the cron expression
            //var cronExpression = CronExpression.Parse("*/2 * * * *");
            // Create a cron expression for executing every day at 3 PM GMT+7
            var cronExpression = CronExpression.Parse(_cronJobOptions.CronExpression);
            var nextOccurrence = cronExpression.GetNextOccurrence(DateTimeOffset.Now, TimeZoneInfo.Local);

            var delay = nextOccurrence - DateTimeOffset.Now;
            _timer.Change((TimeSpan)delay, TimeSpan.FromMilliseconds(-1));

            // update report project status job
            _logger.LogInformation($"[CRON JOB - DoWork] Report Project Status! Start At : {DateTimeHelper.GetCurrentDateTime()}");
            var updateReportProjectStatus = await ProjectStatusUpdater();

            _logger.LogInformation($"[CRON JOB - DoWork] Status Update Report : {updateReportProjectStatus.Status}");
            _logger.LogInformation($"[CRON JOB - DoWork] Msg Update Report : {updateReportProjectStatus.ExMessage}");

            _logger.LogInformation($"[CRON JOB - DoWork] Report Project Status! End At : {DateTimeHelper.GetCurrentDateTime()}");
            // end update report project status job

            _logger.LogInformation($"[CRON JOB - DoWork] Cron job executed! End At : {DateTimeHelper.GetCurrentDateTime()}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop the timer
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // Dispose the timer
            _timer?.Dispose();
        }

        public async Task ExecuteAsync()
        {
            // Job execution logic goes here
            Console.WriteLine("[ExecuteAsync] Cron job executed!");
            _logger.LogInformation($"[CRON JOB - ExecuteAsync] Cron job executed! Start At : {DateTimeHelper.GetCurrentDateTime()}");

            // update report project status job
            _logger.LogInformation($"[CRON JOB - DoWork] Report Project Status! Start At : {DateTimeHelper.GetCurrentDateTime()}");
            var updateReportProjectStatus = await ProjectStatusUpdater();

            _logger.LogInformation($"[CRON JOB - DoWork] Status Update Report : {updateReportProjectStatus.Status}");
            _logger.LogInformation($"[CRON JOB - DoWork] Msg Update Report : {updateReportProjectStatus.ExMessage}");

            _logger.LogInformation($"[CRON JOB - DoWork] Report Project Status! End At : {DateTimeHelper.GetCurrentDateTime()}");
            // end update report project status job

            _logger.LogInformation($"[CRON JOB - ExecuteAsync] Cron job executed! End At : {DateTimeHelper.GetCurrentDateTime()}");
            // Do something...
        }

        public async Task<ReturnViewModel> ProjectStatusUpdater()
        {
            DateTime curentDateActive = DateTimeHelper.GetCurrentDateTime();
            ReturnViewModel result = new ReturnViewModel();

            // Create a scope and resolve the scoped service (DbpmsbjbContext)
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DbMediaServicesContext>();

                // write some query
            }

            return result;
        }
    }
}
