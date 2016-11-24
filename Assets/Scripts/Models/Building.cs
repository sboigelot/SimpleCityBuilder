using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class Building : IClonable<Building>
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public int YearAvalable { get; set; }

        [XmlElement("CityStatImpact")]
        public List<CityStatImpact> CityStatImpacts { get; set; }

        [XmlAttribute]
        public int BuildPrice { get; set; }

        [XmlAttribute]
        public string SpritePath { get; set; }

        [XmlAttribute]
        public string UpgradeName { get; set; }

        [XmlAttribute]
        public int UpgradePrice { get; set; }

        [XmlAttribute]
        public bool Buildable { get; set; }

        public Building Clone()
        {
            return new Building
            {
                Name = Name,
                YearAvalable = YearAvalable,
                CityStatImpacts = CityStatImpacts.Select(cs => cs.Clone()).ToList(),
                BuildPrice = BuildPrice,
                SpritePath = SpritePath,
                UpgradeName = UpgradeName,
                Buildable = Buildable
            };
        }
    }
}