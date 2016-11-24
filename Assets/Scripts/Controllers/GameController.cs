using System.Linq;
using Assets.Scripts.Localization;
using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class GameController : MonoBehaviour
    {
        public GameController()
        {
            Instance = this;
        }

        public static GameController Instance { get; set; }

        public Scenario CurrentScenario { get; set; }

        public City CurrentCity { get; set; }

        public void Awake()
        {
            PrototypeManager.Instance.LoadPrototypes();
            SaveManager.Instance.LoadPlayerProfile();

            Localizer.Instance.EnsureAllLocalKeyExist();
        }

        public void NewGame(string scenarioName)
        {
            var scenario = PrototypeManager.Instance.Scenarios.FirstOrDefault(s => s.Name == scenarioName);
            if (scenario != null)
            {
                CurrentScenario = scenario;
                CurrentCity = scenario.City.Clone();
            }
        }
    }
}