using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class PlayerProfile : IClonable<PlayerProfile>
    {
        [XmlAttribute]
        public string Login { get; set; }

        [XmlAttribute]
        public string Email { get; set; }

        [XmlAttribute]
        public string Score { get; set; }

        [XmlAttribute]
        public bool IsRegistered { get; set; }

        [XmlAttribute]
        public bool RememberPassword { get; set; }

        [XmlAttribute]
        public string PasswordHash { get; set; }

        [XmlElement("ObtainedAchievement")]
        public List<string> ObtainedAchievements { get; set; }

        [XmlAttribute]
        public string Culture { get; set; }

        public PlayerProfile Clone()
        {
            return new PlayerProfile
            {
                Login = Login,
                Email = Email,
                Score = Score,
                IsRegistered = IsRegistered,
                RememberPassword = RememberPassword,
                PasswordHash = PasswordHash,
                ObtainedAchievements = ObtainedAchievements.ToList(),
            };
        }
    }
}