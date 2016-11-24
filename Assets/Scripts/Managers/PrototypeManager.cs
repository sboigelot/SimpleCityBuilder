using System.Collections.Generic;
using Assets.Scripts.Models;
using Assets.Scripts.Serialization;

namespace Assets.Scripts.Managers
{
    public class PrototypeManager : Singleton<PrototypeManager>
    {
        public PlayerProfile PlayerTemplate { get; set; }

        public List<Achievement> Achievements { get; set; }
        
        public List<Building> Buildings { get; set; }

        public List<Scenario> Scenarios { get; set; }

        public void LoadPrototypes()
        {
            PlayerTemplate = Load<PlayerProfile>("PlayerTemplate.xml");
            Achievements = Load<List<Achievement>>("Achievements.xml");
            Buildings = Load<List<Building>>("Buildings.xml");
            Scenarios = Load<List<Scenario>>("Scenarios.xml");
        }

        private T Load<T>(string fileName) where T : class, new()
        {
            return DataSerializer.Instance.LoadFromStreamingAssets<T>("Data", fileName);
        }
    }
}