using System;

namespace LumberJacking.Input
{
    [Flags]
    public enum PlayerAction
    {
        None = 0,
        Forward = 1,
        Backward = 2,
        Left = 4,
        Right = 8,
        Run = 16,
        Attack = 32
    }
}