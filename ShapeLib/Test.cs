using ShapeLib;
using Window;
using System.Numerics;

namespace Test
{
    public static class Test
    {
        public static void Main(string[] args)
        {
            // Square sq = new Square();
            // sq.Draw();
            // sq.Draw();
            // sq.Draw(true);
            // sq.Render();

            Square sq1 = new Square(new Vector2(-0.5f, 0.0f), new Vector2(0.4f, 0.4f));
            sq1.Wireframe = true;

            Square sq2 = new Square(new Vector2(0.5f, 0.0f), new Vector2(0.4f, 0.4f));

            Square sq3 = new Square(new Vector2(0.0f, -0.4f), new Vector2(0.4f, 0.4f));
            sq3.SetColor(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));

            Square sq4 = new Square(new Vector2(0.0f, 0.4f), new Vector2(0.4f, 0.4f));
            sq4.SetColor(new Vector4(1.0f, 0.0f, 1.0f, 1.0f));


            Circle cir1 = new Circle(new Vector2(0.0f, 0.0f), 0.15f, _segments:32);
            cir1.SetColor(new Vector4(0.0f, 1.0f, 0.0f, 1.0f));



            Circle cir3 = new Circle(new Vector2(-0.75f, 0.75f), 0.1f, _segments:5);
            cir3.SetColor(new Vector4(0.0f, 1.0f, 0.5f, 1.0f));

            Circle cir4 = new Circle(new Vector2(0.75f, -0.75f), 0.1f, _segments:6);
            cir4.SetColor(new Vector4(0.0f, 1.0f, 0.5f, 1.0f));

            Circle cir5 = new Circle(new Vector2(0.75f, 0.75f), 0.1f, _segments:7);
            cir5.SetColor(new Vector4(0.0f, 1.0f, 0.5f, 1.0f));

            Triangle tri1 = new Triangle(new Vector2[] {
                new Vector2(-0.5f, -0.5f),
                new Vector2(-1.0f, -0.5f),
                new Vector2(-1.0f, -1.0f),
            });

            tri1.SetColor(new Vector4(1.0f, 0.5f, 0.5f, 1.0f));

            List<Shape> shapes = new List<Shape>
            {
                sq1,
                sq2,
                sq3,
                sq4,
                cir1,
                cir3,
                cir4,
                cir5,
                tri1
            };

            foreach (Shape shape in shapes)
            {
                shape.Move(new Vector2(0.2f, 0.2f));
                shape.Draw(gl:false);
            }
 
            using (
                TestWindow.Window game = new TestWindow.Window(
                    640, 640, "HelloTriangle",
                    new ShapeLogic(shapes))
                )
            {
                game.Run();
            }
     
        }
    }
}