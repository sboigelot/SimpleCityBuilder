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

        public Transform BuildingLayerPanel;

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
            //RandomText.text = Localizer.Get("cityScreen_RandomText");
        }

        public void FixedUpdate()
        {
            if (GameController.Instance.CurrentCity != null)
            {
                ClearInterface();
                BuildInterface();
            }
        }

        private void ClearInterface()
        {
            var city = GameController.Instance.CurrentCity;
            var buildings = city.Buildings;

            var childrens = BuildingLayerPanel.Cast<Transform>().ToList();
            foreach (var child in childrens)
            {
                var info = child.GetComponent<CityScreenBuildingInfo>();
                if (info != null)
                {
                    if (buildings.All(b => b.Id != info.BuildingId))
                    {
                        Destroy(child.gameObject);
                    }
                }
            }
        }

        private void BuildInterface()
        {
            var city = GameController.Instance.CurrentCity;
            var buildings = city.Buildings;

            var childrens = BuildingLayerPanel.Cast<Transform>().ToList();
            foreach (var building in buildings)
            {
                var building1 = building;
                if (childrens.All(c => c.GetComponent<CityScreenBuildingInfo>().BuildingId != building1.Id))
                {
                    var go = new GameObject(building1.BuildingName + " - " + building1.Id);

                    var info = go.AddComponent<CityScreenBuildingInfo>();
                    info.BuildingId = building1.Id;

                    go.transform.position = new Vector3(building1.X, 0f, building1.Z);
                    go.transform.SetParent(BuildingLayerPanel, true);

                    var sr = go.AddComponent<SpriteRenderer>();
                    sr.color = Color.white;

                    var buildingPrototype =
                        PrototypeManager.Instance.Buildings.FirstOrDefault(b => b.Name == building1.BuildingName);
                    if (buildingPrototype != null)
                    {
                        sr.sprite = SpriteManager.Get(buildingPrototype.SpritePath);
                    }

                    go.transform.localScale = new Vector3(1f, 1f, 1f); // Config needs to be added to XML for size?
                }
            }
        }
    }
}