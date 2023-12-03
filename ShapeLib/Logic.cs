using System.Numerics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using WS_ENGINE_BASE;

namespace Window
{
    public class Logic
    {
        public Vector2 Size = new Vector2(0.0f, 0.0f);

        public Camera? camera;

        public void SetCamera(ref Camera _camera)
        {
            this.camera = _camera;
        }
        public void SetRotation(ref OpenTK.Mathematics.Vector2 _rotation)
        {
            // this.camera = _camera;
        }

        public virtual void OnUpdateFrame(FrameEventArgs args, KeyboardState key, MouseState mouse)
        {

        }

        public virtual void OnRenderFrame(FrameEventArgs args, Camera camera, OpenTK.Mathematics.Vector2 modelRotation)
        {

        }

        public virtual void OnLoad()
        {

        }

        public virtual void OnUnload()
        {

        }

        public virtual void OnResize()
        {

        }
    }
}