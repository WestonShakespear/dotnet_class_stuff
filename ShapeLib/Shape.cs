
using System.Numerics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ShapeLib
{
    public class Shape
    {
        protected Vector2 Origin;
        protected Vector4 Color;
        protected uint[]? Triangles;
        protected float[]? Vertices;

        protected int DrawLength = 0;

        public int VertexDataBufferObject;
        public int ElementBufferObject;
        public int VertexArrayObject;

        public bool Wireframe = false;

        protected bool Drawn;

        public Shape()
        {
            this.Origin = new Vector2(0.0f, 0.0f);
            this.Color = new Vector4(0.0f, 0.0f, 1.0f, 1.0f);
        }
        public Shape(Vector2 _origin)
        {
            this.Origin = _origin;
            this.Color = new Vector4(0.0f, 0.0f, 1.0f, 1.0f);
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

        public void Move(Vector2 distance)
        {
            this.Origin += distance;
        }

        
        public Vector2 GetOrigin()
        {
            return this.Origin;
        }
        public void SetOrigin(Vector2 _origin)
        {
            this.Origin = _origin;
        }

        public Vector4 GetColor()
        {
            return this.Color;
        }
        public void SetColor(Vector4 _color)
        {
            this.Color = _color;
        }

    }
}