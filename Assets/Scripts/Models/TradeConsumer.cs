using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class TradeConsumer : IClonable<TradeConsumer>
    {
        [XmlAttribute]
        public string Id { get; set; }

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

        private GameDate _availabilityStart;

        public GameDate AvailabilityStart
        {
            get
            {
                if (_availabilityStart == null)
                {
                    _availabilityStart = new GameDate(AvailabilityStartString);
                }
                return _availabilityStart;
            }

            set { _availabilityStart = value; }
        }

        [XmlAttribute("AvailabilityEnd")]
        public string AvailabilityEndString { get; set; }

        private GameDate _availabilityEnd;

        public GameDate AvailabilityEnd
        {
            get
            {
                if (_availabilityEnd == null)
                {
                    _availabilityEnd = new GameDate(AvailabilityEndString);
                }
                return _availabilityEnd;
            }

            set { _availabilityEnd = value; }
        }

        public TradeConsumer Clone()
        {
            return new TradeConsumer
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