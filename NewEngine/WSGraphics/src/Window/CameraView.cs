using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;
using static WSGraphics.Window.Common;

namespace WSGraphics.Window;

public class CameraView : BaseWindow
{
    private Camera MainCamera;



    public float StartZ = 1.15f;

    public OpenTK.Mathematics.Vector2 ModelRotation = new OpenTK.Mathematics.Vector2(0.0f, 0.0f);

    bool MiddleMouse = false;
    private bool _firstMove = true;
    private OpenTK.Mathematics.Vector2 _lastPos;

    float RotationSensitivity = 0.6f;

    const float cameraSpeed = 0.05f;
    private float LastScroll = 0;

    public CameraView(WindowInitSettings _windowInitSettings) : base(_windowInitSettings)
    {
        MainCamera = new Camera(OpenTK.Mathematics.Vector3.UnitZ * StartZ, Size.X / (float)Size.Y);
        // _windowInitSettings.Logic.MainCamera = MainCamera;
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        Console.WriteLine("CameraWindow OnRenderFrame");
        // Clear the screen
        GL.Clear(ClearBufferMask.ColorBufferBit);

        Logic.DoRenderFrame(args, MainCamera, ModelRotation);

        // Do this last to display the changes
        Context.SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        Console.WriteLine("CameraWindow OnUpdateFrame");
        // base.OnUpdateFrame(args);

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
            MainCamera = new Camera(OpenTK.Mathematics.Vector3.UnitZ * StartZ, Size.X / (float)Size.Y);
            ModelRotation = new OpenTK.Mathematics.Vector2(0.0f, 0.0f);
        }

        bool rotate = mouse[MouseButton.Middle] && !key.IsKeyDown(Keys.LeftShift);
        bool pan = mouse[MouseButton.Middle] && key.IsKeyDown(Keys.LeftShift);

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

                MainCamera.Position += deltaY * (MainCamera.Up * h);
                MainCamera.Position -= deltaX * (OpenTK.Mathematics.Vector3.Normalize(OpenTK.Mathematics.Vector3.Cross(MainCamera.Front,MainCamera.Up)) * w);
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
            MainCamera.Position += MainCamera.Front * cameraSpeed; // Forward
        }
        if (diff == -1)
        {
            MainCamera.Position -= MainCamera.Front * cameraSpeed; // Backwards
        }
        LastScroll = current_scroll;

        Logic.DoUpdateFrame(args, key, mouse);
    }
}