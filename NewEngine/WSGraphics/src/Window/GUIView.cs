using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;

using ImGuiNET;

using static WSGraphics.Window.Common;
using WSGraphics.GUI;

namespace WSGraphics.Window;

public class GUIView : BaseWindow
{
    protected ImGuiController UIController;
    protected BaseGUI GUI;
    protected int FBO;
    protected int FramebufferTexture;
    protected float CamWidth = 800f;
    protected float CamHeight = 600f;

    public GUIView(WindowInitSettings _windowInitSettings, BaseGUI _gui) : base(_windowInitSettings)
    {

        UIController = new ImGuiController(
            (int)WindowWidth, (int)WindowHeight,
            _windowInitSettings.FontPath,
            _windowInitSettings.FontSize
        );

        GUI = _gui;
    }

    protected override void WindowLoad()
    {
        GenerateFBO();
        Logic.DoLoad();
        GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, FramebufferTexture, 0);
    }

    protected override void WindowRenderFrame(FrameEventArgs args)
    {
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, FBO);
        GL.Clear(ClearBufferMask.ColorBufferBit);
        Logic.DoRenderFrame(args, null, null);

        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        UIController.Update(this, (float)args.Time);
        ImGui.DockSpaceOverViewport();


        GUI.RenderGUI(ref CamWidth, ref CamHeight, ref FramebufferTexture);
        Logic.Size.X = CamWidth;
        Logic.Size.Y = CamHeight;
        // GUI.LogWindow(logData);

        UIController.Render();
        ImGuiController.CheckGLError("End of frame");

        // Do this last to display the changes
        Context.SwapBuffers();

        Console.WriteLine("{0}\tx\t{1}\t\t{2}\tx\t{3}", WindowWidth, WindowHeight, CamWidth, CamHeight);
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

        GL.DeleteFramebuffer(FBO);
        GenerateFBO();
        GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, FramebufferTexture, 0);

        UIController.WindowResized((int)WindowWidth, (int)WindowHeight);

        // Resize the gl viewport when the window is resized
        GL.Viewport(0, 0, WindowWidth, WindowHeight);
        

        Logic.DoResize(e);
    }

    private void GenerateFBO()
    {
        FBO = GL.GenFramebuffer();
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, FBO);

        // Color Texture
        FramebufferTexture = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, FramebufferTexture);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb16f, WindowWidth, WindowHeight, 0, PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
    }
}