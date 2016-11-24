namespace Assets.Scripts
{
    public interface IClonable<out T>
    {
        T Clone();
    }
}