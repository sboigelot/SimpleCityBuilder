using System.Linq;
using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class Goal : IClonable<Goal>
    {
        [XmlAttribute]
        public string CityStatName { get; set; }

        [XmlAttribute]
        public int TargetValue { get; set; }

        [XmlAttribute]
        public GoalComparisonType ComparisonType { get; set; }

        public Goal Clone()
        {
            return new Goal
            {
                CityStatName = CityStatName,
                TargetValue = TargetValue,
                ComparisonType = ComparisonType
            };
        }

        public bool IsMet(City city)
        {
            var stat = city.Stats.FirstOrDefault(s => s.Name == CityStatName);
            if (stat == null)
            {
                return false;
            }

            switch (ComparisonType)
            {
                case GoalComparisonType.IsGreaterOrEqual:
                    return stat.Value >= TargetValue;

                case GoalComparisonType.IsLowerOrEqual:
                    return stat.Value <= TargetValue;
            }

            return false;
        }
    }
}