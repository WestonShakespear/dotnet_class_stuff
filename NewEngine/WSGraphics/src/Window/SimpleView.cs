using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;

using static WSGraphics.Window.Common;

namespace WSGraphics.Window;

public class SimpleView : BaseWindow
{
    public SimpleView(WindowInitSettings _windowInitSettings) : base(_windowInitSettings)
    {

    }

    protected override void WindowLoad()
    {
        Logic.DoLoad();
    }

    protected override void WindowRenderFrame(FrameEventArgs args)
    {
        // Clear the screen
        GL.Clear(ClearBufferMask.ColorBufferBit);

        Logic.DoRenderFrame(args, null, null);

        // Do this last to display the changes
        Context.SwapBuffers();
    }

    protected override void WindowUpdateFrame(FrameEventArgs args)
    {
        KeyboardState key = KeyboardState;
        MouseState mouse = MouseState;

        Logic.DoUpdateFrame(args, key, mouse);
    }

    protected override void WindowResize(ResizeEventArgs e)
    {
        WindowHeight = e.Height;
        WindowWidth = e.Width;

        // Resize the gl viewport when the window is resized
        GL.Viewport(0, 0, WindowWidth, WindowHeight);
        Logic.Size = new System.Numerics.Vector2(e.Width, e.Height);

        Logic.DoResize(e);
    }
}