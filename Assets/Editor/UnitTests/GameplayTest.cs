using Assets.Scripts.Controllers;
using Assets.Scripts.Managers;
using NUnit.Framework;

namespace Assets.Editor.UnitTests
{
    public class GameplayTest
    {
        [Test]
        public void StartDefaultNewGame()
        {
            PrototypeManager.Instance.LoadPrototypes();
            SaveManager.Instance.LoadPlayerProfile();
            GameController.Instance.NewGame("Sandbox");
        }
    }
}