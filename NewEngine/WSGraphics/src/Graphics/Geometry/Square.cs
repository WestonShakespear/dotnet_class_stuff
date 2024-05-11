
using System.Numerics;

namespace WSGraphics.Graphics.Geometry;

public class Square : Shape
{
    protected Vector2 Size;

    public Square()
    {
        Origin = new Vector3(0.0f, 0.0f, 0.0f);
        Size = new Vector2(1.0f, 1.0f);

        Draw(gl:false);
    }
    public Square(Vector2 _origin, Vector2 _size)
    {
        Origin = new Vector3(_origin.X, _origin.Y, 0.0f);
        Size = _size;

        Draw(gl:false);
    }

    public override void Draw(bool force = false, bool gl = true)
    {
        float halfX = Size.X / 2.0f;
        float halfY = Size.Y / 2.0f;

        Vertices = new float[]
        {
            Origin.X + halfX, Origin.Y + halfY, 0.0f, // top right
            Origin.X + halfX, Origin.Y - halfY, 0.0f, // bottom right
            Origin.X - halfX, Origin.Y - halfY, 0.0f, // bottom left
            Origin.X - halfX, Origin.Y + halfY, 0.0f  // top left
        };

        Triangles = new uint[]
        {
            0, 1, 3,
            1, 2, 3
        };

        DrawLength = Triangles.Length;
        if (gl) base.Draw();
    }

    public override void Render(int shader_handle)
    {
        if (!Drawn)
        {
            Draw();
        }
        base.Render(shader_handle);
    }


    public Vector2 GetSize()
    {
        return Size;
    }
    public void SetSize(Vector2 _size)
    {
        Size = _size;
    }
}
