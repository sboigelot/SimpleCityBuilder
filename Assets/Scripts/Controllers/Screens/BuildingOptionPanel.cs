using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers.Screens
{
    public class BuildingOptionPanel : MonoBehaviour
    {
        public Button Button;
        public Text Text;
        public Image Sprite;

        private Building building;

        public void RefreshInterface(Building b)
        {
            building = b;
            Sprite.sprite = SpriteManager.Get(b.SpritePath);
            Text.text = b.Name + "(" + b.BuildPrice + ")";
        }

        public void OnButtonClick()
        {
            MouseController.Instance.BuildingUnderConstruction = building;
            MouseController.Instance.Mode = MouseMode.Build;
        }
    }
}