using System;

public class PositionalEventArgs : EventArgs
{
    public int X { get; }

    public int Y { get; }

    public PositionalEventArgs(int x, int y)
    {
        X = x;
        Y = y;
    }
}