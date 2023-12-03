
using System.Numerics;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

using Window;

using WS_ENGINE_BASE;

namespace TestWindow
{
    public class CameraWindow : GameWindow
    {

        
        
        Logic MyLogic;


        Camera camera;
        public float StartZ = 2f;

        public OpenTK.Mathematics.Vector2 ModelRotation = new OpenTK.Mathematics.Vector2(0.0f, 0.0f);

        bool MiddleMouse = false;
        private bool _firstMove = true;
        private OpenTK.Mathematics.Vector2 _lastPos;
        int WindowWidth;
        int WindowHeight;
        float RotationSensitivity = 0.6f;

        const float cameraSpeed = 0.05f;
        const float sensitivity = 0.2f;
        

        public CameraWindow(int width, int height, string title, Logic logic) : base(
            GameWindowSettings.Default,
            new NativeWindowSettings()
            {
                Size = (width,height),
                Title = title
            })
        {
            
           
            this.camera = new Camera(OpenTK.Mathematics.Vector3.UnitZ * StartZ, Size.X / (float)Size.Y);
            this.WindowHeight = Size.Y;
            this.WindowWidth = Size.X;

            this.MyLogic = logic;
            this.MyLogic.SetCamera(ref camera);
            // this.MyLogic.SetRotation(ref ModelRotation);
            this.MyLogic.Size = new System.Numerics.Vector2(Size.X, Size.Y);

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

            

            MyLogic.OnRenderFrame(args, this.camera, this.ModelRotation);

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

            if (!IsFocused) // Check to see if the window is focused
            {
                return;
            }

            if (key.IsKeyPressed(Keys.F))
            {
                this.camera = new Camera(OpenTK.Mathematics.Vector3.UnitZ * StartZ, Size.X / (float)Size.Y);
                this.ModelRotation = new OpenTK.Mathematics.Vector2(0.0f, 0.0f);
            }

            bool rotate = mouse[MouseButton.Middle] && !key.IsKeyDown(Keys.LeftControl);
            bool pan = mouse[MouseButton.Middle] && key.IsKeyDown(Keys.LeftControl);

            if (pan)
            {
                if (!this.MiddleMouse)
                {
                    this.MiddleMouse = true;
                    _lastPos = new OpenTK.Mathematics.Vector2(mouse.X, mouse.Y);
                }
                else
                {
                    float deltaX = mouse.X - _lastPos.X;
                    float deltaY = mouse.Y - _lastPos.Y;
                    _lastPos = new OpenTK.Mathematics.Vector2(mouse.X, mouse.Y);

                    float h = 2.0f / WindowHeight;
                    float w = 2.0f / WindowWidth;

                    this.camera.Position += deltaY * (this.camera.Up * h);
                    this.camera.Position -= deltaX * (OpenTK.Mathematics.Vector3.Normalize(OpenTK.Mathematics.Vector3.Cross(this.camera.Front, this.camera.Up)) * w);
                }

            }
            else
            {
                this.MiddleMouse = false;
            }


            if (rotate)
            {
                if (_firstMove) // this bool variable is initially set to true
                {
                    _lastPos = new OpenTK.Mathematics.Vector2(mouse.X, mouse.Y);
                    _firstMove = false;
                }
                else
                {
                    var deltaX = mouse.X - _lastPos.X;
                    var deltaY = mouse.Y - _lastPos.Y;
                    _lastPos = new OpenTK.Mathematics.Vector2(mouse.X, mouse.Y);

                    
                    this.ModelRotation.X += deltaY * RotationSensitivity; 
                    this.ModelRotation.Y += deltaX * RotationSensitivity;
                }
            }
            else
            {
                _firstMove = true;
            }

            float current_scroll = mouse.Scroll.Y;
            float diff = current_scroll - LastScroll;
            
            if (diff == 1)
            {
                this.camera.Position += this.camera.Front * cameraSpeed; // Forward
            }
            if (diff == -1)
            {
                this.camera.Position -= this.camera.Front * cameraSpeed; // Backwards
            }
            LastScroll = current_scroll;

            MyLogic.OnUpdateFrame(args, key, mouse);
        }

        private float LastScroll = 0;

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
            this.MyLogic.Size = new System.Numerics.Vector2(e.Width, e.Height);

            this.WindowHeight = e.Height;
            this.WindowWidth = e.Width;

            MyLogic.OnResize();
        }

        

    }

    
}