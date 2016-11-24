using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class City : IClonable<City>
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute("Date")]
        public string DateString { get; set; }

        private GameDate _date;
        public GameDate Date
        {
            get
            {
                if (_date == null)
                {
                    _date = new GameDate(DateString);
                }
                return _date;
            }

            set
            {
                _date = value;
            }
        }

        [XmlElement("Stat")]
        public List<CityStat> Stats { get; set; }

        [XmlElement("Building")]
        public List<CityBuilding> Buildings { get; set; }

        [XmlElement("ActiveTrade")]
        public List<Trade> ActiveTrades { get; set; }

        [XmlElement("AvailableTradeProducer")]
        public List<TradeProducer> AvailableTradeProducers { get; set; }

        [XmlElement("AvailableTradeConsumer")]
        public List<TradeConsumer> AvailableTradeConsumers { get; set; }

        public City Clone()
        {
            return new City
            {
                Name = Name,
                DateString = Date.ToString(),
                Buildings = Buildings.Select(b => b.Clone()).ToList(),
                Stats = Stats.Select(s => s.Clone()).ToList(),
                ActiveTrades = ActiveTrades.Select(t => t.Clone()).ToList(),
                AvailableTradeConsumers = AvailableTradeConsumers.Select(c => c.Clone()).ToList(),
                AvailableTradeProducers = AvailableTradeProducers.Select(p => p.Clone()).ToList()
            };
        }
    }
}