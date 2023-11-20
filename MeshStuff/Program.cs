using ShapeLib;
using Window;
using System.Numerics;
using ObjLoader.Loader.Loaders;
using System.Drawing;

namespace Test
{
    public static class Test
    {

        public static void Main(string[] args)
        {
            
           ProjStuff.Generate("simple_noise.obj");

            using (
                TestWindow.CameraWindow game = new TestWindow.CameraWindow(
                    1000, 1000, "HelloTriangle",
                    new MeshStuff_Logic())
                )
            {
                game.Run();
            }
     
        }
    }
}