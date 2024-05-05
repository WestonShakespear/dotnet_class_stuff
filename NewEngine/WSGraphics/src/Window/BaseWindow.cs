using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;


using static WSGraphics.Window.Common;

namespace WSGraphics.Window;

public class BaseWindow : GameWindow
{
    int WindowWidth;
    int WindowHeight;

    ViewLogic Logic;

    public BaseWindow(WindowInitSettings _windowInitSettings) : base(
        GameWindowSettings.Default,
        new NativeWindowSettings()
        {
            Size = ((int)_windowInitSettings.Size.X, (int)_windowInitSettings.Size.Y),
            Title = _windowInitSettings.Title
        })
    {
        
        
        WindowHeight = Size.Y;
        WindowWidth = Size.X;

        Logic = _windowInitSettings.Logic;
        Logic.Size = new System.Numerics.Vector2(Size.X, Size.Y);

        WindowState = _windowInitSettings.WindowInitState;

    }

    protected override void OnLoad()
    {
        base.OnLoad();
        
        GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

        Logic.OnLoad();
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        // Clear the screen
        GL.Clear(ClearBufferMask.ColorBufferBit);

        Logic.OnRenderFrame(args, null, null);

        // Do this last to display the changes
        Context.SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        KeyboardState key = KeyboardState;
        MouseState mouse = MouseState;

        

        Logic.OnUpdateFrame(args, key, mouse);
    }
    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        WindowHeight = e.Height;
        WindowWidth = e.Width;

        // Resize the gl viewport when the window is resized
        GL.Viewport(0, 0, WindowWidth, WindowHeight);
        // Logic.Size = new System.Numerics.Vector2(e.Width, e.Height);

        Logic.OnResize(e);
    }
}