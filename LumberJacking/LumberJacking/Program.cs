using System;

namespace LumberJacking
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new LumberJackingGame())
                game.Run();
        }
    }
}