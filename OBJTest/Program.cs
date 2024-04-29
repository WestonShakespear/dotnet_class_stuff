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
            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create();

            var fileStream = File.OpenRead("motor.obj");
            var result = objLoader.Load(fileStream);

            Console.WriteLine("");

            List<Shape> shapes = new List<Shape>();

            Line originX = new Line(new Vector2(0.0f), new Vector2(0.0f), new Vector2(0.2f, 0.0f), 0.01f);
            Line originY = new Line(new Vector2(0.0f), new Vector2(0.0f), new Vector2(0.0f, 0.2f), 0.01f);
            originX.SetColor(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));
            originY.SetColor(new Vector4(0.0f, 1.0f, 0.0f, 1.0f));
            shapes.Add(originX);
            shapes.Add(originY);

            IList<ObjLoader.Loader.Data.Elements.Face> faces = result.Groups.First().Faces;
            IList<ObjLoader.Loader.Data.VertexData.Vertex> vertices = result.Vertices;

            Vector3[] Points = new Vector3[vertices.Count];
            int index = 0;

            float max = 0.0f;
            const float master_scale = 1.0f;
            float scale = 1.0f;

            foreach (ObjLoader.Loader.Data.VertexData.Vertex v in vertices)
            {
                if ((float)Math.Abs(v.X) > max) max = v.X;
                if ((float)Math.Abs(v.Y) > max) max = v.Y;
                if ((float)Math.Abs(v.Z) > max) max = v.Z;
                // Console.WriteLine("Vertex:  x:{0}  y:{1}  z:{2}", v.X, v.Y, v.Z);
                Vector3 point = new Vector3(v.X, v.Y, v.Z);
                Points[index++] = point;

                
            }
            scale /= max;
            scale *= master_scale;

            foreach (ObjLoader.Loader.Data.VertexData.Vertex v in vertices)
            {
                Vector3 point = new Vector3(v.X * scale, v.Y * scale, v.Z * scale);
                Circle cir = new Circle(point, 0.02f);
                cir.SetColor(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));
                // shapes.Add(cir);
            }

            List<Vector3[]> tris = new List<Vector3[]>();

            foreach (ObjLoader.Loader.Data.Elements.Face face in faces)
            {
                // Console.WriteLine("Count: {0}", face.Count);

                List<Vector3> current_vert = new List<Vector3>();

                for(int i = 0; i < face.Count; i++)
                {
                    current_vert.Add(Points[face[i].VertexIndex-1]);
                    // Console.WriteLine("    v: {0}", face[i].VertexIndex);
                }

                if (current_vert.Count == 4)
                {
                    tris.Add(
                        new Vector3[] {
                            new Vector3(current_vert[0].X*scale, current_vert[0].Y*scale, current_vert[0].Z*scale),
                            new Vector3(current_vert[1].X*scale, current_vert[1].Y*scale, current_vert[1].Z*scale),
                            new Vector3(current_vert[2].X*scale, current_vert[2].Y*scale, current_vert[2].Z*scale),
                        });
                    tris.Add(
                        new Vector3[] {
                            new Vector3(current_vert[0].X*scale, current_vert[0].Y*scale, current_vert[0].Z*scale),
                            new Vector3(current_vert[2].X*scale, current_vert[2].Y*scale, current_vert[2].Z*scale),
                            new Vector3(current_vert[3].X*scale, current_vert[3].Y*scale, current_vert[3].Z*scale),
                        });
                }
                else
                {
                    tris.Add(
                        new Vector3[] {
                            new Vector3(current_vert[0].X*scale, current_vert[0].Y*scale, current_vert[0].Z*scale),
                            new Vector3(current_vert[1].X*scale, current_vert[1].Y*scale, current_vert[1].Z*scale),
                            new Vector3(current_vert[2].X*scale, current_vert[2].Y*scale, current_vert[2].Z*scale),
                        });
                }

                
                    
           
                // shapes.Add(new Circle(new Vector3(v.X, v.Y, v.Z), 0.1f));

            }

            
           

            Triangles tri1 = new Triangles(tris);
            tri1.Wireframe = true;
            shapes.Add(tri1);

          
            
            

     

            using (
                TestWindow.CameraWindow game = new TestWindow.CameraWindow(
                    1000, 1000, "HelloTriangle",
                    new OBJ_Logic(shapes))
                )
            {
                game.Run();
            }
     
        }
    }
}