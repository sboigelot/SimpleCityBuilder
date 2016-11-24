namespace Assets.Scripts.Localization
{
    public interface ILocalizableInterface
    {
        void RegisterLocalizableInterface();

        void OnLocalChanged();
    }
}