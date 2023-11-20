using System.Numerics;

namespace ShapeLib
{
    public class Triangles : Shape
    {
        private List<Vector3[]> Tris = new List<Vector3[]>();

        public int Show = -1;

        public Triangles()
        {
            base.Origin = new Vector3(0.0f, 0.0f, 0.0f);
            this.Tris.Add(new Vector3[]{
                new Vector3(0.0f, 0.0f, 0.0f),
                new Vector3(-0.5f, 0.0f, 0.0f),
                new Vector3(-0.5f, -0.5f, 0.0f),
            });

            this.Draw(gl:false);
        }
        public Triangles(Vector2[] _points)
        {
            base.Origin = new Vector3(0.0f, 0.0f, 0.0f);
            this.Tris.Add(new Vector3[]{
                new Vector3(_points[0].X, _points[0].Y, 0.0f),
                new Vector3(_points[1].X, _points[1].Y, 0.0f),
                new Vector3(_points[2].X, _points[2].Y, 0.0f),
            });

            this.Draw(gl:false);
        }

        public Triangles(Vector3[] _points)
        {
            base.Origin = new Vector3(0.0f, 0.0f, 0.0f);
            this.Tris.Add(_points);

            this.Draw(gl:false);
        }

        public Triangles(List<Vector3[]> _tris)
        {
            base.Origin = new Vector3(0.0f, 0.0f, 0.0f);
            this.Tris = _tris;

            this.Draw(gl:false);
        }


        public override void Draw(bool force = false, bool gl = true)
        {
            // Console.WriteLine("Triangle count: {0}", this.Tris.Count);
            // Console.WriteLine("    vert count: {0}", this.Tris.Count * 9);
            // Console.WriteLine("    tri count: {0}", this.Tris.Count * 3);
            // Console.WriteLine("");
            // Console.WriteLine("");

            int length = this.Tris.Count;

            if (this.Show != -1 && this.Show < this.Tris.Count) length = Show;

            base.Vertices = new float[length * 9];
            base.Triangles = new uint[length * 3];

            for (int i = 0; i < length; i++)
            {
                // Console.WriteLine("TRIANGLE: {0}", i);
                for (int v = 0; v < 3; v++)
                {
                    int a = (i * 9) + (v * 3) + 0;
                    int b = (i * 9) + (v * 3) + 1;
                    int c = (i * 9) + (v * 3) + 2;
                    int nvert = v + (i * 3);

                    // Console.WriteLine("    Vert: {0}    X: {1}", nvert, a);
                    // Console.WriteLine("    Vert: {0}    Y: {1}", nvert, b);
                    // Console.WriteLine("    Vert: {0}    Z: {1}", nvert, c);
                    Vector3 pos = this.CalculateVertexPosition(i, v);
                    base.Vertices[ a ] = pos.X;
                    base.Vertices[ b ] = pos.Y;
                    base.Vertices[ c ] = pos.Z;

                    base.Triangles[ (i * 3) + v ] = (uint)nvert;
                }

                // Console.WriteLine("");
                // Console.WriteLine("");
            }
            
            base.DrawLength = Triangles.Length;
            if (gl) base.Draw();
        }

        private Vector3 CalculateVertexPosition(int _triangle, int _vertex)
        {
            Vector3 vertex = new Vector3(0.0f);
            Vector3 vertex_data = this.Tris[_triangle][_vertex];

            // x, y, axis
            int[][] comm = new int[][]
            {
                new int[] {0, 1, 2},
                new int[] {0, 2, 1},
                new int[] {1, 2, 0}
            };

           
            for (int i = 0; i < 3; i++)
            {
                vertex[i] = vertex_data[i] + this.Origin[i];
            }
            for (int i = 0; i < 3; i++)
            {
                int[] cur = comm[i];

                if (base.Rotation[cur[2]] == 0.0f) continue;

                float angle_rad = base.Rotation[cur[2]] * ((float)Math.PI / 180);
  
                float sin = (float)Math.Sin(angle_rad);
                float cos = (float)Math.Cos(angle_rad);

                vertex[cur[0]] = this.Origin[cur[0]] + vertex_data[cur[0]] * cos - vertex_data[cur[1]] * sin;
                vertex[cur[1]] = this.Origin[cur[1]] + vertex_data[cur[1]] * cos + vertex_data[cur[0]] * sin;
            }

            return vertex;
        }

        public List<Vector3[]> GetExactTriangles()
        {
            List<Vector3[]> triangles = new List<Vector3[]>();
            int i = 0;
            foreach (Vector3[] triangle in this.Tris)
            {
                triangles.Add(new Vector3[]
                    {
                    this.CalculateVertexPosition(i, 0),
                    this.CalculateVertexPosition(i, 1),
                    this.CalculateVertexPosition(i, 2),
                    });
                i++;
            }

            return triangles;
        }

        public override void Render(int shader_handle)
        {
            if (!base.Drawn)
            {
                this.Draw();
            }
            base.Render(shader_handle);
        }

        public List<Vector3[]> GetTriangles()
        {  
            return Tris;
        }

    }
}