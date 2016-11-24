using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class Achievement : IClonable<Achievement>
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlElement("Goal")]
        public List<Goal> Goals { get; set; }

        public bool IsGoalRelated
        {
            get { return Goals.Any(); }
        }

        public Achievement Clone()
        {
            return new Achievement
            {
                Name = Name,
                Goals = Goals.Select(g => g.Clone()).ToList(),
            };
        }
    }
}