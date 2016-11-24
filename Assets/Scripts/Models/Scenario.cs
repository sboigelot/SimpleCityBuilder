using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class Scenario : IClonable<Scenario>
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlElement("City")]
        public City City { get; set; }

        [XmlAttribute("StartDate")]
        public string StartDateString { get; set; }

        private GameDate _startDate;
        public GameDate StartDate
        {
            get
            {
                if (_startDate == null)
                {
                    _startDate = new GameDate(StartDateString);
                }
                return _startDate;
            }

            set
            {
                _startDate = value;
            }
        }

        [XmlAttribute("EndDate")]
        public string EndDateString { get; set; }

        private GameDate _endDate;
        public GameDate EndDate
        {
            get
            {
                if (_endDate == null)
                {
                    _endDate = new GameDate(EndDateString);
                }
                return _endDate;
            }

            set
            {
                _endDate = value;
            }
        }

        [XmlElement("VictoryCondition")]
        public List<Goal> VictoryConditions { get; set; }

        [XmlElement("DefeatCondition")]
        public List<Goal> DefeatConditions { get; set; }

        [XmlElement("RequireAchievementName")]
        public List<string> RequireAchievementNames { get; set; }

        [XmlAttribute]
        public string VictoryAchievement { get; set; }

        public Scenario Clone()
        {
            return new Scenario
            {
                Name = Name,
                City = City.Clone(),
                StartDateString = StartDate.ToString(),
                EndDateString = EndDate.ToString(),
                VictoryConditions = VictoryConditions.Select(g => g.Clone()).ToList(),
                DefeatConditions = DefeatConditions.Select(g => g.Clone()).ToList(),
                RequireAchievementNames = RequireAchievementNames.ToList(),
                VictoryAchievement = VictoryAchievement
            };
        }
    }
}