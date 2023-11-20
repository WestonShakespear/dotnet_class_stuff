
using System.Numerics;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Window;

namespace TestWindow
{
    public class Window : GameWindow
    {

        
        
        Logic MyLogic;
        

        public Window(int width, int height, string title, Logic logic) : base(
            GameWindowSettings.Default,
            new NativeWindowSettings()
            {
                Size = (width,height),
                Title = title
            })
        {
            this.MyLogic = logic;
            this.MyLogic.Size = new Vector2(Size.X, Size.Y);
        }

        

        protected override void OnLoad()
        {
            base.OnLoad();

            // Set the clear color for refreshing
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            
            MyLogic.OnLoad();
        }

        

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            // Clear the screen
            GL.Clear(ClearBufferMask.ColorBufferBit);

            MyLogic.OnRenderFrame(args, new WS_ENGINE_BASE.Camera(new OpenTK.Mathematics.Vector3(0.0f, 0.0f, 0.0f), 0.0f), new OpenTK.Mathematics.Vector2(0.0f));

            // Do this last to display the changes
            Context.SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            KeyboardState key = KeyboardState;
            MouseState mouse = MouseState;

            if (key.IsKeyDown(Keys.Escape))
            {
                Close();
                Environment.Exit(0);
            }

            MyLogic.OnUpdateFrame(args, key, mouse);
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            MyLogic.OnUnload();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            // Resize the gl viewport when the window is resized
            GL.Viewport(0, 0, e.Width, e.Height);
            this.MyLogic.Size = new Vector2(e.Width, e.Height);

            MyLogic.OnResize();
        }

        

    }

    
}