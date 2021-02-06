public class PositionalWithSourceEventArgs : PositionalEventArgs
{
    public int SrcX { get; }

    public int SrcY { get; }

    public PositionalWithSourceEventArgs(int x, int y, int srcX, int srcY )
        :base(x, y)
    {
        SrcX = srcX;
        SrcY = srcY;
    }
}
