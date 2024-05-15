namespace starter_serv.Helper
{
    public static class DateTimeHelper
    {
        public static DateTime GetCurrentDateTime()
        {
            // Get local time zone
            var timeZone = TimeZoneInfo.Local;

            // Get current date and time
            var currentDateTime = DateTime.Now;

            // Format date and time with time zone
            //var formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss zzz", timeZone);

            // Return the formatted date and time
            return currentDateTime;
        }
    }
}
