using System.Linq;
using Assets.Scripts.Controllers.Screens;
using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class BuildingDisplayController : MonoBehaviourSingleton<BuildingDisplayController>
    {
        public Transform BuildingLayerPanel;

        // TODO priority:lower this method of redrawing the building list may not be the most efficient, we should probably store an internal list instead of looping the childrens.
        public void Refresh()
        {
            var city = GameController.Instance.CurrentCity;
            if (city != null)
            {
                RemoveBuildings(city);
                AddBuildings(city);
            }
        }

        private void RemoveBuildings(City city)
        {
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

        private void AddBuildings(City city)
        {
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

                    go.transform.position = new Vector3(building1.X, building1.Y, 0f);
                    go.transform.SetParent(BuildingLayerPanel, true);

                    var sr = go.AddComponent<SpriteRenderer>();
                    sr.color = Color.white;

                    var buildingPrototype =
                        PrototypeManager.Instance.Buildings.FirstOrDefault(b => b.Name == building1.BuildingName);
                    if (buildingPrototype != null)
                    {
                        sr.sprite = SpriteManager.Get(buildingPrototype.SpritePath);
                    }

                    go.transform.localScale = new Vector3(.5f, .5f, .5f); // Config needs to be added to XML for size?
                }
            }
        }
    }
}