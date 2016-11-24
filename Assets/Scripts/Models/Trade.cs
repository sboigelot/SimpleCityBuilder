using System;
using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class Trade : IClonable<Trade>
    {
        [XmlAttribute]
        public int TradeProducerId { get; set; }

        [XmlAttribute]
        public int TradeConsumerId { get; set; }

        [XmlAttribute]
        public int TradeCapacityConsumed { get; set; }

        [XmlAttribute]
        public int WeeklyProfit { get; set; }

        [XmlAttribute]
        public bool RepackagingCapacityConsumed { get; set; }

        public Trade Clone()
        {
            return new Trade
            {
                TradeProducerId = TradeProducerId,
                TradeConsumerId = TradeConsumerId,
                TradeCapacityConsumed = TradeCapacityConsumed,
                WeeklyProfit = WeeklyProfit,
                RepackagingCapacityConsumed = RepackagingCapacityConsumed
            };
        }
    }
}