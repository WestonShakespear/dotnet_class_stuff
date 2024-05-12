using System.Numerics;

using WSGraphics.Window;
using static WSGraphics.Window.Common;
using WSGraphics.Graphics;


namespace Program;

class Program
{
    public static void Main(string[] args)
    {
        bool DoGUI = true;

        string root = @"C:\Users\Initec\github-repos\";

        TestLogic tl = new TestLogic(
            new Shader(
                    Path.Combine(root, @"dotnet_class_stuff\NewEngine\Tests\TestShape\cam.vert"),
                    Path.Combine(root, @"dotnet_class_stuff\NewEngine\Tests\TestShape\cam.frag")
                ));
        TestGUI gui = new TestGUI();

        WindowInitSettings win = new WindowInitSettings(tl)
        {
            Size = new Vector2(2000, 1000),
            Title = "Test 1 WSGraphics",
            WindowInitState = OpenTK.Windowing.Common.WindowState.Maximized
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