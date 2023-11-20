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

            // Square sq1 = new Square(new Vector2(-0.5f, 0.0f), new Vector2(0.4f, 0.4f));
            // sq1.Wireframe = true;

            // Square sq2 = new Square(new Vector2(0.5f, 0.0f), new Vector2(0.4f, 0.4f));

            // Square sq3 = new Square(new Vector2(0.0f, -0.4f), new Vector2(0.4f, 0.4f));
            // sq3.SetColor(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));

            // Square sq4 = new Square(new Vector2(0.0f, 0.4f), new Vector2(0.4f, 0.4f));
            // sq4.SetColor(new Vector4(1.0f, 0.0f, 1.0f, 1.0f));


            // Circle cir1 = new Circle(new Vector2(0.0f, 0.0f), 0.15f, _segments:32);
            // cir1.SetColor(new Vector4(0.0f, 1.0f, 0.0f, 1.0f));



            // Circle cir3 = new Circle(new Vector2(-0.75f, 0.75f), 0.1f, _segments:5);
            // cir3.SetColor(new Vector4(0.0f, 1.0f, 0.5f, 1.0f));

            // Circle cir4 = new Circle(new Vector2(0.75f, -0.75f), 0.1f, _segments:6);
            // cir4.SetColor(new Vector4(0.0f, 1.0f, 0.5f, 1.0f));

            // Circle cir5 = new Circle(new Vector2(0.75f, 0.75f), 0.1f, _segments:7);
            // cir5.SetColor(new Vector4(0.0f, 1.0f, 0.5f, 1.0f));

            List<Vector3[]> tris = new List<Vector3[]>
            {
                new Vector3[] {
                new Vector3(-0.5f, -0.5f, 0.1f),
                new Vector3(-1.0f, -0.5f, 0.1f),
                new Vector3(-1.0f, -1.0f, -0.1f),
                },
                
            };

            // tris.Add(new Vector3[] {
            // new Vector3(0.5f, -0.5f, 0.1f),
            // new Vector3(1.0f, -0.5f, 0.1f),
            // new Vector3(1.0f, -1.0f, -0.1f),
            // });
            // tris.Add(new Vector3[] {
            // new Vector3(-0.5f, -.5f, 0.1f),
            // new Vector3(-1.0f, -.5f, 0.1f),
            // new Vector3(-1.0f, -.0f, -0.1f),
            // });

            // tris.Add(new Vector3[] {
            // new Vector3(0.5f, 0.5f, 0.1f),
            // new Vector3(1.0f, 0.5f, -0.1f),
            // new Vector3(1.0f, 1.0f, 0.1f),
            // });
            float amount = 0.0f;
            Vector3 rot = new Vector3(0.0f, 90.0f, 90.0f);

            Triangles tri1 = new Triangles(tris);
            tri1.Move(new Vector3(amount, amount, 0.0f));

            Triangles tri2 = new Triangles(tris);
            tri2.Move(new Vector3(amount, amount, 0.0f));
            tri2.Rotate(rot);

            Triangles tri3 = new Triangles(tris);
            tri3.Move(new Vector3(amount, amount, 0.0f));
            tri3.Rotate(2*rot);

            Triangles tri4 = new Triangles(tris);
            tri4.Move(new Vector3(amount, amount, 0.0f));
            tri4.Rotate(3*rot);

            Line l = new Line(
                new Vector3(0.0f),
                new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(1.0f, 1.0f, 0.0f),
                0.01f);
            l.Wireframe = true;

            // Line l1 = new Line(
            //     new Vector2(0.0f, 0.0f), 
            //     new Vector2(0.0f, 0.0f),
            //     new Vector2(0.5f, 0.5f), 0.1f);

            // tri1.SetColor(new Vector4(1.0f, 0.5f, 0.5f, 1.0f));

            List<Shape> shapes = new List<Shape>
            {
                // sq1,
                // sq2,
                // sq3,
                // sq4,
                // cir1,
                // cir3,
                // cir4,
                // cir5,
                // tri1,
                // tri2,
                // tri3,
                // tri4,
                l
                
                // l1
            };

            // foreach (Shape shape in shapes)
            // {
            //     shape.Move(new Vector2(0.2f, 0.2f));
            //     shape.Draw(gl:false);
            // }
 
            // using (
            //     TestWindow.Window game = new TestWindow.Window(
            //         640, 640, "HelloTriangle",
            //         new ShapeLogic(shapes))
            //     )
            // {
            //     game.Run();
            // }

            using (
                TestWindow.CameraWindow game = new TestWindow.CameraWindow(
                    640, 640, "HelloTriangle",
                    new ShapeLogic(shapes))
                )
            {
                game.Run();
            }
     
        }
    }
}