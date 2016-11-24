using Assets.Scripts.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers.Screens
{
    public class MainMenuScreenBehaviour : MonoBehaviour, ILocalizableInterface
    {
        public Text StartGameButtonText;

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
            StartGameButtonText.text = Localizer.Get("mainMenu_StartGameButtonText");
        }

        public void StartGame()
        {
            GameController.Instance.NewGame("Sandbox");
            ScreenController.Instance.SwitchToScreen(ScreenController.Instance.CityScreen);
        }
    }
}