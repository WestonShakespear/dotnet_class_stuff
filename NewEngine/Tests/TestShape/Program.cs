using System.Numerics;

using WSGraphics.Window;
using static WSGraphics.Window.Common;
using WSGraphics.Graphics;


namespace Program;

class Program
{
    public static void Main(string[] args)
    {
        bool DoGUI = false;

        TestLogic tl = new TestLogic(
            new Shader(
                    @"C:\Users\Initec\github-repos\dotnet_class_stuff\NewEngine\Tests\TestShape\cam.vert",
                    @"C:\Users\Initec\github-repos\dotnet_class_stuff\NewEngine\Tests\TestShape\cam.frag"
                ));
        TestGUI gui = new TestGUI();

        WindowInitSettings win = new WindowInitSettings(tl)
        {
            Size = new Vector2(2000, 1000),
            Title = "Test 1 WSGraphics",
            WindowInitState = OpenTK.Windowing.Common.WindowState.Fullscreen
        };
        


        if (DoGUI)
        {
            using (GUIView view = new GUIView(win, gui))
            {
                view.Run();
            }
        }
        else
        {
            using (SimpleView view = new SimpleView(win))
            {
                view.Run();
            }
        }

        
    }

}