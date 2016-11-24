using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class ScreenController : MonoBehaviourSingleton<ScreenController>
    {
        public GameObject MainMenuScreen;

        public GameObject CityScreen;

        public GameObject TradeScreen;

        public GameObject LoginScreen;

        public GameObject RegisterScreen;

        public GameObject EndGameScreen;
        
        public void Awake()
        {
            SwitchToScreen(MainMenuScreen);
        }

        public void SwitchToScreen(GameObject screen)
        {
            MainMenuScreen.SetActive(false);
            CityScreen.SetActive(false);
            TradeScreen.SetActive(false);
            LoginScreen.SetActive(false);
            RegisterScreen.SetActive(false);
            EndGameScreen.SetActive(false);
            screen.SetActive(true);
        }
    }
}