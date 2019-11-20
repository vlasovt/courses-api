using System;

namespace CourseLibrary.API.Helpers
{
    public static class DateTimeExtensions
    {
        public static int GetAge(this DateTimeOffset dateTime)
        {
            var currentDate = DateTime.Today;
            var age = currentDate.Year - dateTime.Year;

            if(currentDate < dateTime.Date)
            {
                age--;
            }

            return age;
        }
    }
}
