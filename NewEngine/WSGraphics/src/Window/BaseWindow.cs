using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;


using static WSGraphics.Window.Common;
using System.Numerics;

namespace WSGraphics.Window;

public class BaseWindow : GameWindow
{
    protected int WindowWidth;
    protected int WindowHeight;
    protected Vector4 ClearColor;

    protected ViewLogic Logic;

    protected WindowState TempWindowState;

    public BaseWindow(WindowInitSettings _windowInitSettings) : base(
        GameWindowSettings.Default,
        new NativeWindowSettings()
        {
            ClientSize = new OpenTK.Mathematics.Vector2i((int)_windowInitSettings.Size.X, (int)_windowInitSettings.Size.Y),
            Title = _windowInitSettings.Title,
            WindowBorder = WindowBorder.Resizable,
            StartVisible = true,
            StartFocused = true,
            API = ContextAPI.OpenGL,
            Profile = ContextProfile.Core,
            APIVersion = new Version(3, 3)
        })
    {
        
        
        WindowHeight = Size.Y;
        WindowWidth = Size.X;
        ClearColor = _windowInitSettings.ClearColor;

        Logic = _windowInitSettings.Logic;
        Logic.Size = new System.Numerics.Vector2(Size.X, Size.Y);

        TempWindowState = _windowInitSettings.WindowInitState;
        // VSync = VSyncMode.On;
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.ClearColor(ClearColor.X, ClearColor.Y, ClearColor.Z, ClearColor.W);
        WindowLoad();
        WindowState = TempWindowState;
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        WindowRenderFrame(args);
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        if (KeyboardState.IsKeyDown(Keys.Escape)) Close();
        WindowUpdateFrame(args);
        
    }
    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        WindowResize(e);
    }


    protected virtual void WindowLoad() {}
    protected virtual void WindowRenderFrame(FrameEventArgs args) {}
    protected virtual void WindowUpdateFrame(FrameEventArgs args) {}
    protected virtual void WindowResize(ResizeEventArgs e) {}
    
}