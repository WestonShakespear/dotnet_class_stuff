using System.Numerics;

namespace WSGraphics.Graphics.Geometry;

public class Triangle : Shape
{
    private Vector3[] Points;

    public Triangle()
    {
        Origin = new Vector3(0.0f, 0.0f, 0.0f);
        Points = new Vector3[]{
            new Vector3(0.0f, 0.0f, 0.0f),
            new Vector3(-0.5f, 0.0f, 0.0f),
            new Vector3(-0.5f, -0.5f, 0.0f),
        };

        Draw(gl:false);
    }
    public Triangle(Vector2[] _points)
    {
        Origin = new Vector3(0.0f, 0.0f, 0.0f);
        Points = new Vector3[]{
            new Vector3(_points[0].X, _points[0].Y, 0.0f),
            new Vector3(_points[1].X, _points[1].Y, 0.0f),
            new Vector3(_points[2].X, _points[2].Y, 0.0f),
        };

        Draw(gl:false);
    }

    public Triangle(Vector3[] _points)
    {
        Origin = new Vector3(0.0f, 0.0f, 0.0f);
        Points = _points;;

        Draw(gl:false);
    }


    public override void Draw(bool force = false, bool gl = true)
    {
        Vertices = new float[]
        {
            Origin.X + Points[0].X, Origin.Y + Points[0].Y, Points[0].Z,
            Origin.X + Points[1].X, Origin.Y + Points[1].Y, Points[1].Z,
            Origin.X + Points[2].X, Origin.Y + Points[2].Y, Points[2].Z,
        };

        Triangles = new uint[]
        {
            0, 1, 2,
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

}
