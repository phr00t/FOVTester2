using Xenko.Engine;

namespace FOVTester2.Windows
{
    class FOVTester2App
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
				game.OverrideDefaultSettings(320, 240, false);
                game.Run();
            }
        }
    }
}
