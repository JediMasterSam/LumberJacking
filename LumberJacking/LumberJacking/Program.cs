using System;
using LumberJacking.World;

namespace LumberJacking
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Level.PrintPath(1);
            // using (var game = new Game1())
            //     game.Run();
        }
    }
}