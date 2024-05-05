using System.Numerics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace WSGraphics.Window;

public class ViewLogic
{
    public Vector2 Size;
    public virtual void OnUpdateFrame(
        FrameEventArgs args, 
        KeyboardState key, 
        MouseState mouse) {}

    public virtual void OnRenderFrame(
        FrameEventArgs args,
        Camera? camera,
        OpenTK.Mathematics.Vector2? modelRotation) {}

    public virtual void OnLoad() {}

    public virtual void OnUnload() {}

    public virtual void OnResize(ResizeEventArgs e)
    {
        Size = new Vector2(e.Width, e.Height);
    }
}
