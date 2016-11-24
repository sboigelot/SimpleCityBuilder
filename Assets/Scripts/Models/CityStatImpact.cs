using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class CityStatImpact : IClonable<CityStatImpact>
    {
        [XmlAttribute]
        public string ParameterName { get; set; }

        [XmlAttribute]
        public int WeeklyImpact { get; set; }

        public CityStatImpact Clone()
        {
            return new CityStatImpact
            {
                ParameterName = ParameterName,
                WeeklyImpact = WeeklyImpact,
            };
        }
    }
}