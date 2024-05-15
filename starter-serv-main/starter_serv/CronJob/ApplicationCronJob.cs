using NCrontab;
using starter_serv.BusinessProviders;
using starter_serv.Helper;

namespace starter_serv.CronJob
{
    public class ApplicationCronJob
    {
        public static void Start()
        {
            // Your application logic goes here

            // Schedule the cron job
            ScheduleCronJob();

            // Keep the application running
            Console.ReadLine();
        }

        static void ScheduleCronJob()
        {
            //var cronExpression = "0 15 * * *"; // 3 PM every day
            var cronExpression = "*/2 * * * *"; // Every 2 minutes

            var cronSchedule = CrontabSchedule.Parse(cronExpression);

            var scheduledTask = new ApplicationScheduledTaskJob();

            var timer = new Timer(_ =>
            {
                if (cronSchedule.GetNextOccurrence(DateTimeHelper.GetCurrentDateTime()) == DateTime.Now)
                {
                    scheduledTask.ExecuteJob();
                }
            }, null, TimeSpan.Zero, TimeSpan.FromMinutes(1)); // Check every minute

            Console.ReadLine(); // Prevent the application from exiting immediately
        }
    }
}
