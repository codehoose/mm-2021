namespace SK2D
{
    public interface IPausable
    {
        bool Paused { get; set; }

        float TimeScale { get; set; }
    }
}
