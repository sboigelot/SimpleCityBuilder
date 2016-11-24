using Assets.Scripts.Models;
using Assets.Scripts.Serialization;

namespace Assets.Scripts.Managers
{
    public class SaveManager : Singleton<SaveManager>
    {
        public PlayerProfile PlayerProfile { get; set; }

        public void LoadPlayerProfile()
        {
            PlayerProfile = DataSerializer.Instance.LoadFromAppData<PlayerProfile>(string.Empty, "Profile.xml");
            if (PlayerProfile == null)
            {
                PlayerProfile = PrototypeManager.Instance.PlayerTemplate.Clone();
                DataSerializer.Instance.SaveToAppData(string.Empty, "Profile.xml", PlayerProfile);
            }
        }

        public void SavePlayerProfile()
        {
            if (PlayerProfile != null)
            {
                DataSerializer.Instance.SaveToAppData(string.Empty, "Profile.xml", PlayerProfile);
            }
        }
    }
}