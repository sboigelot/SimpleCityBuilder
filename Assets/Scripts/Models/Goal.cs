using System;
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
    }
}