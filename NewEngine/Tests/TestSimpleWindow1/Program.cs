using System.Numerics;
using WSGraphics.Window;
using static WSGraphics.Window.Common;

namespace Program;

class Program
{
    public static void Main(string[] args)
    {
        WindowInitSettings win = new WindowInitSettings(
            new Vector2(1000, 1000),
            "Test 1 WSGraphics",
            new TestLogic()
        );

        using (SimpleView view = new SimpleView(win))
        {
            view.Run();
        }
    }

}