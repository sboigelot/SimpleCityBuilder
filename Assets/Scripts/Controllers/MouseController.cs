using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class MouseController : MonoBehaviourSingleton<MouseController>
    {
        public MouseMode Mode = MouseMode.Navigate;

        public Building BuildingUnderConstruction;

        public SpriteRenderer BuildingGhostSprite;
        
        public void Update()
        {
            Vector2 mousePosition = Input.mousePosition;

            if (Mode == MouseMode.Build)
            {
                Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                
                BuildingGhostSprite.sprite = SpriteManager.Get(BuildingUnderConstruction.SpritePath);
                BuildingGhostSprite.color = new Color(1f, 1f, 1f, 0.5f);
                BuildingGhostSprite.gameObject.SetActive(true);

                BuildingGhostSprite.transform.position = worldMousePosition;

                if (Input.GetMouseButtonDown(0))
                {
                    GameController.Instance.CurrentCity.Build(BuildingUnderConstruction, worldMousePosition);

                    BuildingUnderConstruction = null;
                    Mode = MouseMode.Navigate;

                    BuildingGhostSprite.gameObject.SetActive(false);
                }
            }
        }
    }
}