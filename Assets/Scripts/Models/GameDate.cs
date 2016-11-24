using System;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class GameDate : IClonable<GameDate>//, IXmlSerializable
    {
        private int _week;
        private int _weekInYear;

        public int Year { get; set; }

        public int Week
        {
            get { return _week; }
            set
            {
                if (value != _week)
                {
                    if (_weekInYear == 0)
                    {
                        _weekInYear = WeekInYear();
                    }

                    if (value > _weekInYear)
                    {
                        Year++;
                        _weekInYear = WeekInYear();
                        _week = 0;
                    }
                    else
                    {
                        _week = value;
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
    }
}