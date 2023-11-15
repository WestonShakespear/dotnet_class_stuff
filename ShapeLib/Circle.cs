using System.Numerics;

namespace ShapeLib
{
    public class Circle : Shape
    {
        protected float Radius;
        protected int Segments;

        public Circle()
        {
            base.Origin = new Vector2(0.0f, 0.0f);
            this.Radius = 0.5f;
            this.Segments = 16;

            this.Draw(gl:false);
        }
        public Circle(Vector2 _origin, float _radius, int _segments = 16)
        {
            base.Origin = _origin;
            this.Radius = _radius;
            this.Segments = _segments;

            this.Draw(gl:false);
        }


        public override void Draw(bool force = false, bool gl = true)
        {
            double angle = 2 * Math.PI / this.Segments;

            base.Vertices = new float[12 + (3 * this.Segments)];
            base.Vertices[0] = base.Origin.X;
            base.Vertices[1] = base.Origin.Y;
            base.Vertices[2] = 0.0f;

            base.Triangles = new uint[3 + (3 * this.Segments)];

            for (int i = 0; i <= this.Segments; i++)
            {
                base.Vertices[3 * (i + 1)] =     base.Origin.X + (this.Radius * (float)Math.Cos(angle * i));//x);
                base.Vertices[3 * (i + 1) + 1] = base.Origin.Y + (this.Radius * (float)Math.Sin(angle * i)); //y;
                base.Vertices[3 * (i + 1) + 2] = 0.0f;                       //z;

                base.Triangles[3 * i] =      0;
                base.Triangles[3 * i + 1] =  (uint)(i + 1);
                base.Triangles[3 * i + 2] =  (uint)(i + 2);

            }
            base.DrawLength = Triangles.Length - 3;
            
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


        public float GetRadius()
        {
            return this.Radius;
        }
        public void SetRadius(float _radius)
        {
            this.Radius = _radius;
        }
    }
}