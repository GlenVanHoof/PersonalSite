namespace PersonalSite.Web.Helpers
{
    public static class DateTimeExtensions
    {
        public static string ToRelativeTime(this DateTime dateTime)
        {
            var timeSpan = DateTime.UtcNow - dateTime;

            if (timeSpan.TotalMinutes < 1)
                return "zojuist";
            if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes} minuten geleden";
            if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours} uur geleden";
            if (timeSpan.TotalDays < 7)
                return $"{(int)timeSpan.TotalDays} dagen geleden";
            if (timeSpan.TotalDays < 30)
                return $"{(int)(timeSpan.TotalDays / 7)} weken geleden";
            if (timeSpan.TotalDays < 365)
                return $"{(int)(timeSpan.TotalDays / 30)} maanden geleden";

            return $"{(int)(timeSpan.TotalDays / 365)} jaar geleden";
        }
    }
}