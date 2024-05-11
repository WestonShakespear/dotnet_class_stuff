using System.Numerics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace WSGraphics.Window;

public class ViewLogic
{
    public Vector2 Size;
    public virtual void DoUpdateFrame(
        FrameEventArgs args, 
        KeyboardState key, 
        MouseState mouse) {}

    public virtual void DoRenderFrame(
        FrameEventArgs args,
        Camera? camera,
        OpenTK.Mathematics.Vector2? modelRotation) {}

    public virtual void DoLoad() {}

    public virtual void DoUnload() {}

    public virtual void DoResize(ResizeEventArgs e) {}
}
