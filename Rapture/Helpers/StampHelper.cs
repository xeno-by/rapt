using System;

namespace Rapture.Helpers
{
    public static class StampHelper
    {
        public static String NewTimestamp()
        {
            var now = DateTime.Now;
            return String.Format("{0}{1}{2}_{3}{4}{5}_{6}",
                now.Year.ToString("0000"),
                now.Month.ToString("00"),
                now.Day.ToString("00"),
                now.Hour.ToString("00"),
                now.Minute.ToString("00"),
                now.Second.ToString("00"),
                now.Millisecond.ToString("000"));
        }
    }
}
