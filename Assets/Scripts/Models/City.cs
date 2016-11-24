using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Assets.Scripts.Controllers;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class City : IClonable<City>
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute("Date")]
        public string DateString { get; set; }

        private GameDate date;

        public GameDate Date
        {
            get { return date ?? (date = new GameDate(DateString)); }
            set { date = value; }
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

        // TODO priority:lower the efficiency of this can be largely improved
        public void EndOfWeek()
        {
            foreach (var cityBuilding in Buildings)
            {
                var building = cityBuilding;
                var buildingPrototype =
                    PrototypeManager.Instance.Buildings.FirstOrDefault(b => b.Name == building.BuildingName);

                foreach (var cityStatImpact in buildingPrototype.CityStatImpacts)
                {
                    ImpactStat(cityStatImpact);
                }
            }

            foreach (var cityStat in Stats)
            {
                if (cityStat.StatType == CityStatType.SumOfBuildings)
                {
                    // TODO priority:high refactor this as use of First is unsafe, adding a reference to the CityBuilding to a Building prototype upon creation will help a lot
                    // TODO priority:bug this doesn't seems to work
                    CityStat stat = cityStat;
                    cityStat.Value =
                        Buildings.SelectMany(
                            bd =>
                                PrototypeManager.Instance.Buildings.First(b => b.Name == bd.BuildingName)
                                    .CityStatImpacts)
                            .Where(s => s.ParameterName == stat.Name)
                            .Sum(i => i.WeeklyImpact);
                }
            }

            var cashImpact = new CityStatImpact { ParameterName = "Cash" };
            foreach (var activeTrade in ActiveTrades)
            {
                cashImpact.WeeklyImpact = activeTrade.WeeklyProfit;
                ImpactStat(cashImpact);
            }

            Date.Week++;
        }

        public void ImpactStat(CityStatImpact cityStatImpact)
        {
            var impactedStat = Stats.FirstOrDefault(s => s.Name == cityStatImpact.ParameterName);

            if (impactedStat != null && impactedStat.StatType != CityStatType.SumOfBuildings)
            {
                impactedStat.Value += cityStatImpact.WeeklyImpact;
            }
        }

        public void Build(Building building, Vector2 worldMousePosition)
        {
            var cityBuilding = new CityBuilding
            {
                BuildingName = building.Name,
                Id = Buildings.Max(b => b.Id) + 1,
                X = worldMousePosition.x,
                Y = worldMousePosition.y,
            };

            Buildings.Add(cityBuilding);

            var cashImpact = new CityStatImpact { ParameterName = "Cash" };
            ImpactStat(cashImpact);
            BuildingDisplayController.Instance.Refresh();
        }
    }
}