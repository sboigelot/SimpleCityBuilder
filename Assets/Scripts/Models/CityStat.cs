using System;
using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class CityStat : IClonable<CityStat>
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string SpritePath { get; set; }

        [XmlAttribute]
        public int Value { get; set; }

        [XmlAttribute]
        public int MinValue { get; set; }

        [XmlAttribute]
        public int MaxValue { get; set; }

        [XmlAttribute]
        public CityStatType StatType { get; set; }

        public CityStat Clone()
        {
            return new CityStat
            {
                Name = Name,
                SpritePath = SpritePath,
                Value = Value,
                MinValue = MinValue,
                MaxValue = MaxValue,
                StatType = StatType,
            };
        }
    }
}