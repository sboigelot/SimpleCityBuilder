using System;
using System.Linq;
using Assets.Scripts.Localization;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers.Screens
{
    public class CityScreenBehaviour : MonoBehaviour, ILocalizableInterface
    {
        public Text RandomText;

        public Transform StatInfoLayerPanel;

        public Transform BuildingListPanel;

        public IntervalTimedAction CityWeekInterval;

        public void Awake()
        {
            RegisterLocalizableInterface();
        }

        public void RegisterLocalizableInterface()
        {
            Localizer.Instance.RegisterInterface(this);
        }

        public void OnLocalChanged()
        {
            // RandomText.text = Localizer.Get("cityScreen_RandomText");
        }

        public void FixedUpdate()
        {
            if (GameController.Instance.CurrentCity == null)
            {
                return;
            }

            CityWeekInterval.Update();
        }

        public void OnEnable()
        {
            CityWeekInterval = new IntervalTimedAction
            {
                Duration = TimeSpan.FromSeconds(GameController.Instance.CurrentScenario.SecondPerWeek),
                Action = EndOfWeek
            };

            if (BuildingDisplayController.Instance != null)
            {
                BuildingDisplayController.Instance.Refresh();
            }

            AddOrUpdateStatsInfoPanel();
            AddOrUpdateBuildingOptionPanel();
        }

        private void EndOfWeek()
        {
            var city = GameController.Instance.CurrentCity;
            city.EndOfWeek();
            GameController.Instance.CurrentScenario.CheckGoals(city);
            AddOrUpdateStatsInfoPanel();
            AddOrUpdateBuildingOptionPanel();
        }

        public void AddOrUpdateStatsInfoPanel()
        {
            var city = GameController.Instance.CurrentCity;
            var stat = city.Stats;

            var childrens = StatInfoLayerPanel.Cast<Transform>().ToList();
            foreach (var child in childrens)
            {
                Destroy(child.gameObject);
            }

            var timeDisplay = (GameObject)Instantiate(Resources.Load("Prefabs/CityStatPanelPrefab"), StatInfoLayerPanel);
            timeDisplay.GetComponent<CityStatPanel>().RefreshInterface(city.Date);

            foreach (var cityStat in stat)
            {
                var go = (GameObject)Instantiate(Resources.Load("Prefabs/CityStatPanelPrefab"), StatInfoLayerPanel);

                var cityStatPanel = go.GetComponent<CityStatPanel>();
                cityStatPanel.RefreshInterface(cityStat);
            }
        }

        private void AddOrUpdateBuildingOptionPanel()
        {
            var childrens = BuildingListPanel.Cast<Transform>().ToList();
            foreach (var child in childrens)
            {
                Destroy(child.gameObject);
            }

            foreach (var building in PrototypeManager.Instance.Buildings)
            {
                if (building.Buildable)
                {
                    var go = (GameObject)Instantiate(Resources.Load("Prefabs/BuildingOptionPrefab"), BuildingListPanel);

                    var buildingOptionPanel = go.GetComponent<BuildingOptionPanel>();
                    buildingOptionPanel.RefreshInterface(building);
                }
            }
        }
    }
}