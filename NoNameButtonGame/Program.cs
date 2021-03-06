using System;

namespace NoNameButtonGame
{
    public static class Program
    {
        [STAThread]
        static void Main(string[] args) {
            if (args.Length > 0) {
                Console.WriteLine("im working!");
            } else {
                using (var game = new NoNameGame())
                    game.Run();
            }
            
        }
    }
}
