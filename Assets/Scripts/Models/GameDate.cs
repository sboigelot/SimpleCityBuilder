using System;
using System.Globalization;
using System.Linq;

namespace Assets.Scripts.Models
{
    public class GameDate : IClonable<GameDate>
    {
        private int week;
        private int weekInYear;

        public int Year { get; set; }

        public int Week
        {
            get
            {
                return week;
            }

            set
            {
                if (value != week)
                {
                    if (weekInYear == 0)
                    {
                        weekInYear = WeekInYear();
                    }

                    if (value > weekInYear)
                    {
                        Year++;
                        weekInYear = WeekInYear();
                        week = 0;
                    }
                    else
                    {
                        week = value;
                    }
                }
            }
        }

        public GameDate(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                var arguments = text.Split(new[] { '-' }).ToList();
                Year = int.Parse(arguments[0]);
                Week = int.Parse(arguments[1]);
            }
        }

        public GameDate()
        {
        }

        private int WeekInYear()
        {
            var currentCulture = CultureInfo.CurrentCulture;
            int weekNo = currentCulture.Calendar.GetWeekOfYear(
                new DateTime(Year, 12, 31),
                currentCulture.DateTimeFormat.CalendarWeekRule,
                currentCulture.DateTimeFormat.FirstDayOfWeek);
            return weekNo;
        }

        public GameDate Clone()
        {
            return new GameDate
            {
                Year = Year,
                Week = Week
            };
        }

        public override string ToString()
        {
            return Year + "-" + Week;
        }
    }
}