using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers.Screens
{
    public class CityStatPanel : MonoBehaviour
    {
        public Image Sprite;
        public Text Text;

        public void RefreshInterface(CityStat cityStat)
        {
            Sprite.sprite = SpriteManager.Get(cityStat.SpritePath);
            Text.text = cityStat.Name + ": " + cityStat.Value;
        }

        public void RefreshInterface(GameDate date)
        {
            Sprite.sprite = SpriteManager.Get("Clock");
            Text.text = date.Year + " - " + date.Week;
        }
    }
}