using System.Linq;
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
        public int WeeklyGoodQuantity { get; set; }

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

        public bool IsValid(City city)
        {
            var tradeCapacityStat = city.Stats.FirstOrDefault(s => s.Name == "TradeCapacity");
            int tradeCapacity = tradeCapacityStat != null ? tradeCapacityStat.Value : 0;
            int usedTradeCapacity = city.ActiveTrades.Sum(t => t.TradeCapacityConsumed);
            int tradeCapacityAvailable = tradeCapacity - usedTradeCapacity;

            if (TradeCapacityConsumed > tradeCapacityAvailable)
            {
                return false;
            }

            var producer = city.AvailableTradeProducers.FirstOrDefault(p => p.Id == TradeProducerId);
            var consumer = city.AvailableTradeConsumers.FirstOrDefault(p => p.Id == TradeConsumerId);

            if (producer == null || consumer == null)
            {
                return false;
            }

            if (producer.WeeklyGoodQuantity < WeeklyGoodQuantity ||
                consumer.WeeklyGoodQuantity < WeeklyGoodQuantity)
            {
                return false;
            }

            return true;
        }

        public void ActivateTrade(City city)
        {
            if (IsValid(city))
            {
                var producer = city.AvailableTradeProducers.FirstOrDefault(p => p.Id == TradeProducerId);
                var consumer = city.AvailableTradeConsumers.FirstOrDefault(p => p.Id == TradeConsumerId);

                if (producer != null && consumer != null)
                {
                    producer.WeeklyGoodQuantity -= WeeklyGoodQuantity;
                    consumer.WeeklyGoodQuantity -= WeeklyGoodQuantity;
                    city.ActiveTrades.Add(this);
                }
            }
        }
    }
}