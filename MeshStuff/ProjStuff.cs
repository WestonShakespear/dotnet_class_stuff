using ShapeLib;
using Window;
using System.Numerics;
using ObjLoader.Loader.Loaders;
using System.Drawing;

namespace Test
{
    public static class ProjStuff
    {

        public static bool focusA = false;
        public static bool focusB = false;
        public static bool projA = false;
        public static bool projB = false;

        public static bool meshA = true;
        public static bool meshB = true;
        public static bool meshC = true;
        public static int set = -1;

        public static List<Shape> shapes = new List<Shape>();

        public static string Filename = "";

        public static void Generate(string _filename)
        {
            shapes = new List<Shape>();
            Filename = _filename;
            

            Circle origin = new Circle(new Vector2(0.0f), 0.05f);
            origin.SetColor(ColorOrigin);
            shapes.Add(origin);

            Vector3 transP = new Vector3(-1.6f, -0.5f, 1.6f);
            Vector3 transPB = new Vector3(transP.X * -1, transP.Y, transP.Z);


            Circle p1 = new Circle(transP, 0.05f);
            p1.SetColor(ColorMeshA);
            shapes.Add(p1);

            Circle p2 = new Circle(transPB, 0.05f);
            p2.SetColor(ColorMeshB);
            shapes.Add(p2);

            List<Vector3[]> tris = GetData(Filename);
           

            Triangles tri1 = new Triangles(tris);
            tri1.Wireframe = true;
            tri1.SetColor(ColorMeshA);
            tri1.Show = set;
            if (meshA)
            {
                shapes.Add(tri1);
            }


            // Make flat
            List<Vector3[]> tris2 = GetFlatTriangles(tri1.GetTriangles());
            List<Vector3[]> tris3 = GetFlatTriangles(tri1.GetTriangles());


            float rotation = 30.0f;
            Vector3 trans = new Vector3(-0.6f, 0.0f, 0.6f);


            Vector3 a_rotate = new Vector3(0.0f, rotation, 0.0f);
            Vector3 a_move = trans;

            Vector3 b_rotate = new Vector3(0.0f, -1*rotation, 0.0f);
            Vector3 b_move = new Vector3(trans.X * -1, trans.Y, trans.Z);

            Triangles tri2 = new Triangles(tris2);
            tri2.Wireframe = true;
            tri2.SetColor(ColorMeshB);
            tri2.Move(a_move);
            tri2.Rotate(a_rotate);
            tri2.Show = set;
            if (meshB)
            {
                shapes.Add(tri2);
            }

            Triangles tri3 = new Triangles(tris2);
            tri3.Wireframe = true;
            tri3.SetColor(ColorMeshC);
            tri3.Move(b_move);
            tri3.Rotate(b_rotate);
            tri3.Show = set;
            if (meshC)
            {
                shapes.Add(tri3);
            }

        
            DrawLines(
                tri1.GetExactTriangles(), 
                tri2.GetExactTriangles(), 
                tri3.GetExactTriangles(),
                transP,
                transPB,
                ref shapes);
     
        }

       


        public static List<Vector3[]> GetFlatTriangles(List<Vector3[]> input)
        {
            List<Vector3[]> tris = new List<Vector3[]>();

            foreach (Vector3[] tri_data in input)
            {
                tris.Add(
                            new Vector3[] {
                                new Vector3(tri_data[0].X, tri_data[0].Y, 0.0f),
                                new Vector3(tri_data[1].X, tri_data[1].Y, 0.0f),
                                new Vector3(tri_data[2].X, tri_data[2].Y, 0.0f),
                            });
            }

            return tris;
        }

        public static void DrawLines(List<Vector3[]> orig_act, List<Vector3[]> flat_act_a, List<Vector3[]> flat_act_b, Vector3 proj_point_a, Vector3 proj_point_b, ref List<Shape> shapes)
        {
            int length = orig_act.Count;
            if (set != -1) length = set;
            

            for (int i = 0; i < length; i++)
            {
                for (int b = 0; b < 3; b++)
                {
                    if (focusA)
                    {
                        Line projection = new Line(
                            new Vector3(0.0f),
                            flat_act_a[i][b],
                            orig_act[i][b],
                        0.01f);
                        projection.SetColor(ColorLineFocusA);
                        shapes.Add(projection);
                    }

                    if (focusB)
                    {
                        Line projection2 = new Line(
                            new Vector3(0.0f),
                            orig_act[i][b],
                            flat_act_b[i][b],
                        0.01f);
                        projection2.SetColor(ColorLineFocusB);
                        shapes.Add(projection2);
                    }

                    if (projA)
                    {
                        Line projection3 = new Line(
                            new Vector3(0.0f),
                            proj_point_a,
                            orig_act[i][b],
                            0.01f);
                        projection3.SetColor(ColorLineProjA);
                        shapes.Add(projection3);
                    }

                    if (projB)
                    {
                        Line projection4 = new Line(
                            new Vector3(0.0f),
                            proj_point_b,
                            orig_act[i][b], 
                        0.01f);
                        projection4.SetColor(ColorLineProjB);
                        shapes.Add(projection4);
                    }
                }
                
            }
        }




        public static List<Vector3[]> GetData(string filename)
        {
            List<Vector3[]> tris = new List<Vector3[]>();

            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create();

            var fileStream = File.OpenRead(filename);
            var result = objLoader.Load(fileStream);


            

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
            }
            return tris;
        }

         static Vector4 ColorOrigin = new Vector4(0.1f, 0.8f, 0.1f, 1.0f);

        static Vector4 ColorMeshA = new Vector4(0.8392f, 0.4784f, 0.0078f, 1.0f);
        static Vector4 ColorMeshB = new Vector4(0.0117f, 0.5882f, 0.145f, 1.0f);
        static Vector4 ColorMeshC = new Vector4(0.1254f, 0.1568f, 0.9806f, 1.0f);

        static Vector4 ColorLineFocusA = new Vector4(0.2588f, 0.5294f, 0.9607f, 1.0f);
        static Vector4 ColorLineFocusB = new Vector4(0.5215f, 0.1803f, 1.0f, 1.0f);

        static Vector4 ColorLineProjA = new Vector4(0.9803f, 0.3725f, 0.4117f, 1.0f);
        static Vector4 ColorLineProjB = new Vector4(0f, 0f, 0f, 1.0f);

        // Line originX = new Line(new Vector2(0.0f), new Vector2(0.0f), new Vector2(0.2f, 0.0f), 0.01f);
        //     Line originY = new Line(new Vector2(0.0f), new Vector2(0.0f), new Vector2(0.0f, 0.2f), 0.01f);
        //     originX.SetColor(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));
        //     originY.SetColor(new Vector4(0.0f, 1.0f, 0.0f, 1.0f));
        //     shapes.Add(originX);
        //     shapes.Add(originY);

        // Get distances
            // List<float[]> distances = new List<float[]>();
            // List<Vector3[]> tr_orig = tri1.GetTriangles();
            // List<Vector3[]> tr_new  = tri2.GetTriangles();

            // for (int i = 0; i < tr_orig.Count; i++)
            // {
            //     float[] cur_tri = new float[3];

            //     for (int b = 0; b < 3; b++)
            //     {
            //         float orig = tr_orig[i][b].Z;
            //         float norm = 0.0f;
            //         cur_tri[b] = orig - norm;
            //     }
            //     distances.Add(cur_tri);
            // }

            // List<Vector3[]> tr3 = new List<Vector3[]>();
            // for (int i = 0; i < tr_orig.Count; i++)
            // {
            //     Vector3[] tri = tr_orig[i];
            //     tr3.Add(
            //                 new Vector3[] {
            //                     new Vector3(tri[0].X, tri[0].Y, distances[i][0] * -1),
            //                     new Vector3(tri[1].X, tri[1].Y, distances[i][1] * -1),
            //                     new Vector3(tri[2].X, tri[2].Y, distances[i][2] * -1),
            //                 });
            // }

            // Triangles tri3 = new Triangles(tr3);
            // tri3.Wireframe = true;
            // tri3.SetColor(new Vector4(0.0f, 1.0f, 0.0f, 1.0f));
            // tri3.Move(new Vector3(2.0f, 0.0f, 0.0f));
            // shapes.Add(tri3);
    }
}