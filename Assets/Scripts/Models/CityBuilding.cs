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
        public float X { get; set; }

        [XmlAttribute]
        public float Y { get; set; }

        public CityBuilding Clone()
        {
            return new CityBuilding
            {
                Id = Id,
                BuildingName = BuildingName,
                X = X,
                Y = Y
            };
        }
    }
}