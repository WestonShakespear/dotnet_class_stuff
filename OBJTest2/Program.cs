using ShapeLib;
using Window;
using System.Numerics;
using ObjLoader.Loader.Loaders;

namespace Test
{
    public static class Test
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("");

            List<Shape> shapes = new List<Shape>();

            


            // Input
            Vector2 initialP = new Vector2(0.0f, 0.0f);

            // commandX, commandY, I, J

            List<Vector4> inputs = new List<Vector4>()
            {
                // Quadrant 1 ++
                new Vector4( 1.0f, -2.0f,  2.0f,  2.0f),//**
                new Vector4( 4.0f, -2.0f,  2.0f,  2.0f),
                new Vector4( 6.0f,  1.0f,  2.0f,  2.0f),
                new Vector4( 6.0f,  4.0f,  2.0f,  2.0f),
                new Vector4(-1.0f,  6.0f,  2.0f,  2.0f),
                new Vector4(-2.0f,  1.0f,  2.0f,  2.0f),

                // Quadrant 2 -+
                new Vector4(-1.0f, -2.0f, -2.0f,  2.0f),//**
                new Vector4(-4.0f, -2.0f, -2.0f,  2.0f),
                new Vector4(-6.0f,  1.0f, -2.0f,  2.0f),
                new Vector4(-6.0f,  4.0f, -2.0f,  2.0f),
                new Vector4( 1.0f,  6.0f, -2.0f,  2.0f),
                new Vector4( 2.0f,  1.0f, -2.0f,  2.0f),

                // Quadrant 3 --
                new Vector4(-1.0f,  2.0f, -2.0f, -2.0f),//**
                new Vector4(-4.0f,  2.0f, -2.0f, -2.0f),
                new Vector4(-6.0f, -1.0f, -2.0f, -2.0f),
                new Vector4(-6.0f, -4.0f, -2.0f, -2.0f),
                new Vector4( 1.0f, -6.0f, -2.0f, -2.0f),
                new Vector4( 2.0f, -1.0f, -2.0f, -2.0f),

                // Quadrant 4 +-
                new Vector4( 1.0f,  2.0f,  2.0f, -2.0f),//**
                new Vector4( 4.0f,  2.0f,  2.0f, -2.0f),
                new Vector4( 6.0f, -1.0f,  2.0f, -2.0f),
                new Vector4( 6.0f, -4.0f,  2.0f, -2.0f),
                new Vector4(-1.0f, -6.0f,  2.0f, -2.0f),
                new Vector4(-2.0f, -1.0f,  2.0f, -2.0f),
            };

            int col = 6;
            float spacing = 20.0f;

            float originHome = -1 * (float)(col/2) * spacing;

            Vector2 origin = new Vector2(originHome, inputs.Count / col / 2 * spacing);

            int c = 1;
            

            foreach (Vector4 vec in inputs)
            {
                Arc(initialP, new Vector2(vec.X, vec.Y), vec.Z, vec.W, origin, ref shapes);

                origin.X += spacing;

                if (c++ % col == 0) { origin.X = originHome; origin.Y -= spacing*1.5f; Console.WriteLine();}
            }

           


            

            


            




            



            using (
                TestWindow.CameraWindow game = new TestWindow.CameraWindow(
                    1920, 1080, "HelloTriangle",
                    new OBJ_Logic(shapes))
                )
            {
                game.Run();
            }
     
        }

        public static void Arc(Vector2 initialP, Vector2 commandP, float I, float J, Vector2 origin, ref List<Shape> shapes)
        {
            float scale = 20.0f;
            int seg = 5;

            float pSize = 0.025f * 10.0f;
            float aSize = 0.01f * 10.0f;

            Vector2 endP = new Vector2(0.0f, 0.0f);

            float radius = (float) Math.Sqrt( (I*I) + (J*J) );

            Vector2 radiusP = new Vector2(initialP.X + I, initialP.Y + J);


            float M = 0.0f;
            

            if (radiusP.X > initialP.X)
            {
                M = (float) (-1 * Math.PI + Math.Asin( J / radius ));
            }
            else
            {
                if (radiusP.Y > initialP.Y)
                {
                    M = (float) (-1 * Math.PI / 2 - Math.Asin( I / radius ));
                }
                else
                {
                    M = (float) (-1 * Math.Asin( J / radius ));
                }
            }

            float N = (float) (-1 * M + Math.Atan( (commandP.Y - radiusP.Y) / (commandP.X - radiusP.X) ));

            if (commandP.X < radiusP.X)
            {
                N += (float) Math.PI; 
            }

            float fullC = (float) (2 * Math.PI);

            while (M < 0) M += fullC;
            while (M > 2 * Math.PI) M -= fullC;

            while (N < 0) N += fullC;
            while (N > 2 * Math.PI) N -= fullC;

            

          

            endP.X = (float) (radiusP.X + radius * Math.Cos(M + N));
            endP.Y = (float) (radiusP.Y + radius * Math.Sin(M + N));

            // Console.WriteLine("M:{0}\t{1}    N:{2}\t{3}", M, N, M*180/Math.PI, N*180/Math.PI);
            

            float start = 0.0f;
            float end = N;

            
            float iter = (end - start) / seg;
            float val = start;

            List<Circle> arcCircles = new List<Circle>();

            for (int i = 0; i <= seg; i++)
            {
                float oX = (float) ( radiusP.X + radius * Math.Cos(M + val));
                float oY = (float) ( radiusP.Y + radius * Math.Sin(M + val));

                arcCircles.Add(new Circle(new Vector2((oX+origin.X) / scale, (oY+origin.Y) / scale), aSize / scale));
          
                val += iter;
            }

            foreach (Circle circ in arcCircles)
            {
                circ.SetColor(new Vector4(0.5f, 0.0f, 0.5f, 1.0f));
                shapes.Add(circ);
            }

            Dictionary<Circle, Vector4> edgeCircles = new Dictionary<Circle, Vector4>()
            {
                { new Circle(new Vector2((initialP.X+origin.X) / scale, (initialP.Y+origin.Y) / scale), pSize / scale), new Vector4(1.0f, 0.0f, 0.0f, 1.0f) },
                { new Circle(new Vector2((commandP.X+origin.X) / scale, (commandP.Y+origin.Y) / scale), pSize / scale), new Vector4(0.0f, 0.0f, 0.0f, 1.0f) },
                { new Circle(new Vector2((radiusP.X+origin.X) / scale, (radiusP.Y+origin.Y) / scale), pSize / scale),   new Vector4(0.0f, 0.0f, 1.0f, 1.0f) },
                { new Circle(new Vector2((endP.X+origin.X) / scale, (endP.Y+origin.Y) / scale), pSize / scale),         new Vector4(0.0f, 1.0f, 0.0f, 1.0f) },
            };

            foreach (KeyValuePair<Circle, Vector4> circ in edgeCircles)
            {
                circ.Key.SetColor(circ.Value);
                shapes.Add(circ.Key);
            }
        }
    }
}