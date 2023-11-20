using System.Numerics;

namespace ShapeLib
{
    public class Triangle : Shape
    {
        private Vector3[] Points;

        public Triangle()
        {
            base.Origin = new Vector3(0.0f, 0.0f, 0.0f);
            this.Points = new Vector3[]{
                new Vector3(0.0f, 0.0f, 0.0f),
                new Vector3(-0.5f, 0.0f, 0.0f),
                new Vector3(-0.5f, -0.5f, 0.0f),
            };

            this.Draw(gl:false);
        }
        public Triangle(Vector2[] _points)
        {
            base.Origin = new Vector3(0.0f, 0.0f, 0.0f);
            this.Points = new Vector3[]{
                new Vector3(_points[0].X, _points[0].Y, 0.0f),
                new Vector3(_points[1].X, _points[1].Y, 0.0f),
                new Vector3(_points[2].X, _points[2].Y, 0.0f),
            };

            this.Draw(gl:false);
        }

        public Triangle(Vector3[] _points)
        {
            base.Origin = new Vector3(0.0f, 0.0f, 0.0f);
            this.Points = _points;;

            this.Draw(gl:false);
        }


        public override void Draw(bool force = false, bool gl = true)
        {
            base.Vertices = new float[]
            {
                base.Origin.X + this.Points[0].X, base.Origin.Y + this.Points[0].Y, this.Points[0].Z,
                base.Origin.X + this.Points[1].X, base.Origin.Y + this.Points[1].Y, this.Points[1].Z,
                base.Origin.X + this.Points[2].X, base.Origin.Y + this.Points[2].Y, this.Points[2].Z,
            };

            base.Triangles = new uint[]
            {
                0, 1, 2,
            };
            
            base.DrawLength = Triangles.Length;
            if (gl) base.Draw();
        }

        public override void Render(int shader_handle)
        {
            if (!base.Drawn)
            {
                this.Draw();
            }
            base.Render(shader_handle);
        }

    }
}