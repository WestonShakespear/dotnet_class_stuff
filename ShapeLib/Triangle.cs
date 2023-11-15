using System.Numerics;

namespace ShapeLib
{
    public class Triangle : Shape
    {
        private Vector2[] Points;

        public Triangle()
        {
            base.Origin = new Vector2(0.0f, 0.0f);
            this.Points = new Vector2[]{
                new Vector2(0.0f, 0.0f),
                new Vector2(-0.5f, 0.0f),
                new Vector2(-0.5f, -0.5f),
            };

            this.Draw(gl:false);
        }
        public Triangle(Vector2[] _points)
        {
            base.Origin = new Vector2(0.0f, 0.0f);
            this.Points = _points;

            this.Draw(gl:false);
        }


        public override void Draw(bool force = false, bool gl = true)
        {
            base.Vertices = new float[]
            {
                base.Origin.X + this.Points[0].X, base.Origin.Y + this.Points[0].Y, 0.0f,
                base.Origin.X + this.Points[1].X, base.Origin.Y + this.Points[1].Y, 0.0f,
                base.Origin.X + this.Points[2].X, base.Origin.Y + this.Points[2].Y, 0.0f,
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