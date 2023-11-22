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
            
           ProjStuff.Generate("wave_complex.obj");

            using (
                TestWindow.CameraWindow game = new TestWindow.CameraWindow(
                    2000, 2000, "HelloTriangle",
                    new MeshStuff_Logic())
                )
            {
                game.Run();
            }
     
        }
    }
}