using System.Numerics;
using OpenTK.Graphics.OpenGL4;

namespace ShapeMath
{
    public class Shape
    {
        protected Vector3 Origin = new Vector3(0.0f, 0.0f, 0.0f);
        protected Vector3 Rotation = new Vector3(0.0f, 0.0f, 0.0f);
        protected Vector4 Color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
        protected uint[]? Triangles;
        protected float[]? Vertices;

        protected int DrawLength = 0;

        public int VertexDataBufferObject;
        public int ElementBufferObject;
        public int VertexArrayObject;

        public bool Wireframe = false;
        public bool Display = true;
        
        protected bool Drawn;


        public Shape()
        {
        }
        public Shape(Vector2 _origin)
        {
            this.Origin = new Vector3(_origin.X, _origin.Y, 0.0f);
        }

        public virtual void Draw(bool force = false, bool gl = true)
        {
            if (!force && this.Drawn)   return;
            if (this.Triangles is not null && this.Vertices is not null)
            {

                this.VertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(this.VertexArrayObject);

                // Create and bind buffer for vertex data
                this.VertexDataBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, this.VertexDataBufferObject);

                // Now that it's bound, load with data
                GL.BufferData(
                    BufferTarget.ArrayBuffer,
                    this.Vertices.Length * sizeof(float),
                    this.Vertices,
                    BufferUsageHint.StaticDraw);

                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);

                this.ElementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.ElementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, Triangles.Length * sizeof(uint), Triangles, BufferUsageHint.StaticDraw);

                this.Drawn = true;
            }
        }

        public virtual void Render(int shader_handle)
        {
            if (!this.Display) return;

            if (this.Wireframe)
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            }


            int vertexColorLocation = GL.GetUniformLocation(shader_handle, "color");
            GL.Uniform4(vertexColorLocation, this.Color.X, this.Color.Y, this.Color.Z, this.Color.W);

            GL.BindVertexArray(this.VertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, DrawLength, DrawElementsType.UnsignedInt, 0);

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
  
        }

        public Vector4 GetColor()
        {
            return this.Color;
        }
        public void SetColor(Vector4 _color)
        {
            this.Color = _color;
        }

        public virtual void Move(Vector3 _distance)
        {
            this.Origin += _distance;
        }
        public virtual void Rotate(Vector3 _rotation)
        {
            this.Rotation += _rotation;
        }

    }
}