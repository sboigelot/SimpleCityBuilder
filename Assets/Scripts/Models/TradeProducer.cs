using System;
using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class TradeProducer : IClonable<TradeProducer>
    {
        [XmlAttribute]
        public int Id { get; set; }

        [XmlAttribute]
        public string CountryName { get; set; }

        [XmlAttribute]
        public string CompanyName { get; set; }

        [XmlAttribute]
        public string GoodName { get; set; }

        [XmlAttribute]
        public int GoodUnitPrice { get; set; }

        [XmlAttribute]
        public int WeeklyGoodQuantity { get; set; }

        [XmlAttribute("AvailabilityStart")]
        public string AvailabilityStartString { get; set; }

        private GameDate availabilityStart;

        public GameDate AvailabilityStart
        {
            get
            {
                if (availabilityStart == null)
                {
                    availabilityStart = new GameDate(AvailabilityStartString);
                }

                return availabilityStart;
            }

            set
            {
                availabilityStart = value;
            }
        }

        [XmlAttribute("AvailabilityEnd")]
        public string AvailabilityEndString { get; set; }

        private GameDate availabilityEnd;

        public GameDate AvailabilityEnd
        {
            get
            {
                if (availabilityEnd == null)
                {
                    availabilityEnd = new GameDate(AvailabilityEndString);
                }

                return availabilityEnd;
            }

            set
            {
                availabilityEnd = value;
            }
        }

        public TradeProducer Clone()
        {
            return new TradeProducer
            {
                Id = Id,
                CountryName = CountryName,
                CompanyName = CompanyName,
                GoodName = GoodName,
                GoodUnitPrice = GoodUnitPrice,
                WeeklyGoodQuantity = WeeklyGoodQuantity,
                AvailabilityStartString = AvailabilityStart.ToString(),
                AvailabilityEndString = AvailabilityEnd.ToString(),
            };
        }
    }
}