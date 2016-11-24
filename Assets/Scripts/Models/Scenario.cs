using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;
using Assets.Scripts.Managers;

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

        private GameDate startDate;

        public GameDate StartDate
        {
            get { return startDate ?? (startDate = new GameDate(StartDateString)); }
            set { startDate = value; }
        }

        [XmlAttribute("EndDate")]
        public string EndDateString { get; set; }

        private GameDate endDate;

        public GameDate EndDate
        {
            get
            {
                if (endDate == null)
                {
                    endDate = new GameDate(EndDateString);
                }

                return endDate;
            }

            set
            {
                endDate = value;
            }
        }

        [XmlAttribute("SecondPerWeek")]
        public int SecondPerWeek { get; set; }

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

        public void CheckGoals(City city)
        {
            // TODO priority:bug this whole method doesn't seems to work
            if (VictoryConditions.All(g => g.IsMet(city)))
            {
                OnVictory();
            }

            if (DefeatConditions.Any(g => g.IsMet(city)))
            {
                OnDefeat();
            }

            foreach (var achievement in PrototypeManager.Instance.Achievements)
            {
                if (achievement.IsGoalRelated)
                {
                    if (achievement.Goals.All(g => g.IsMet(city)))
                    {
                        OnAchievement(achievement);
                    }
                }
            }
        }

        private void OnAchievement(Achievement achievement)
        {
            // TODO priority:medium
            Debug.WriteLine("Achievement get: " + achievement.Name);
            if (!SaveManager.Instance.PlayerProfile.ObtainedAchievements.Contains(achievement.Name))
            {
                SaveManager.Instance.PlayerProfile.ObtainedAchievements.Add(achievement.Name);
                SaveManager.Instance.SavePlayerProfile();
            }
        }

        private void OnDefeat()
        {
            // TODO priority:high
            Debug.WriteLine("Defeat");
        }

        private void OnVictory()
        {
            // TODO priority:high
            Debug.WriteLine("Victory");
        }
    }
}