
using System.Numerics;

namespace ShapeLib
{
    public class Square : Shape
    {
        protected Vector2 Size;

        public Square()
        {
            base.Origin = new Vector3(0.0f, 0.0f, 0.0f);
            this.Size = new Vector2(1.0f, 1.0f);

            this.Draw(gl:false);
        }
        public Square(Vector2 _origin, Vector2 _size)
        {
            base.Origin = new Vector3(_origin.X, _origin.Y, 0.0f);
            this.Size = _size;

            this.Draw(gl:false);
        }

        public override void Draw(bool force = false, bool gl = true)
        {
            float halfX = this.Size.X / 2.0f;
            float halfY = this.Size.Y / 2.0f;

            base.Vertices = new float[]
            {
                base.Origin.X + halfX, base.Origin.Y + halfY, 0.0f, // top right
                base.Origin.X + halfX, base.Origin.Y - halfY, 0.0f, // bottom right
                base.Origin.X - halfX, base.Origin.Y - halfY, 0.0f, // bottom left
                base.Origin.X - halfX, base.Origin.Y + halfY, 0.0f  // top left
            };

            base.Triangles = new uint[]
            {
                0, 1, 3,
                1, 2, 3
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


        public Vector2 GetSize()
        {
            return this.Size;
        }
        public void SetSize(Vector2 _size)
        {
            this.Size = _size;
        }
    }
}