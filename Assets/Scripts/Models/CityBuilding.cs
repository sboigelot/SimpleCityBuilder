using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class CityBuilding : IClonable<CityBuilding>
    {
        [XmlAttribute]
        public int Id { get; set; }

        [XmlAttribute]
        public string BuildingName { get; set; }

        [XmlAttribute]
        public int X { get; set; }

        [XmlAttribute]
        public int Z { get; set; }

        public CityBuilding Clone()
        {
            return new CityBuilding
            {
                Id = Id,
                BuildingName = BuildingName,
                X = X,
                Z = Z
            };
        }
    }
}