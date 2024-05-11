using System.Numerics;
using OpenTK.Graphics.OpenGL4;

namespace WSGraphics.Graphics.Geometry;
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
        Origin = new Vector3(_origin.X, _origin.Y, 0.0f);
    }

    public virtual void Draw(bool force = false, bool gl = true)
    {
        if (!force && Drawn)   return;
        if (Triangles is not null && Vertices is not null)
        {
            // for (int i = 0; i < this.Vertices.Length; i++)
            // {
            //     Console.Write("{0},", this.Vertices[i]);
            //     if (i % 3 == 0) Console.WriteLine();
            // }

            // Console.WriteLine();
            

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            // Create and bind buffer for vertex data
            VertexDataBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexDataBufferObject);

            // Now that it's bound, load with data
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                Vertices.Length * sizeof(float),
                Vertices,
                BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Triangles.Length * sizeof(uint), Triangles, BufferUsageHint.StaticDraw);

            Drawn = true;
        } else { Console.WriteLine("nulll"); }
    }

    public virtual void Render(int shader_handle)
    {
        if (!Display) return;

        if (Wireframe)
        {
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
        }


        int vertexColorLocation = GL.GetUniformLocation(shader_handle, "vertexColor");
        GL.Uniform4(vertexColorLocation, Color.X, Color.Y, Color.Z, Color.W);

        GL.BindVertexArray(VertexArrayObject);
        GL.DrawElements(PrimitiveType.Triangles, DrawLength, DrawElementsType.UnsignedInt, 0);

        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

    }

    public Vector4 GetColor()
    {
        return Color;
    }
    public void SetColor(Vector4 _color)
    {
        Color = _color;
    }

    public virtual void Move(Vector3 _distance)
    {
        Origin += _distance;
    }
    public virtual void SetOrigin(Vector3 _origin)
    {
        Origin = _origin;
    }
    public virtual Vector3 GetOrigin() { return Origin; }
    public virtual void Rotate(Vector3 _rotation)
    {
        Rotation += _rotation;
    }

}
