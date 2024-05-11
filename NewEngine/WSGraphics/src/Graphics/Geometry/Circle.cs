using System.Numerics;

namespace WSGraphics.Graphics.Geometry;

public class Circle : Shape
{
    protected float Radius;
    protected int Segments;

    public Circle()
    {
        Origin = new Vector3(0.0f, 0.0f, 0.0f);
        Radius = 0.25f;
        Segments = 16;

        Draw(gl:false);
    }
    public Circle(Vector2 _origin, float _radius, int _segments = 16)
    {
        Origin = new Vector3(_origin.X, _origin.Y, 0.0f);
        Radius = _radius;
        Segments = _segments;

        Draw(gl:false);
    }

    public Circle(Vector3 _origin, float _radius, int _segments = 16)
    {
        Origin = _origin;
        Radius = _radius;
        Segments = _segments;

        Draw(gl:false);
    }


    public override void Draw(bool force = false, bool gl = true)
    {
        double angle = 2 * Math.PI / Segments;

        Vertices = new float[12 + (3 * Segments)];
        Vertices[0] = Origin.X;
        Vertices[1] = Origin.Y;
        Vertices[2] = Origin.Z;

        Triangles = new uint[3 + (3 * Segments)];

        for (int i = 0; i <= Segments; i++)
        {
            Vertices[3 * (i + 1)] =     Origin.X + (Radius * (float)Math.Cos(angle * i));//x);
            Vertices[3 * (i + 1) + 1] = Origin.Y + (Radius * (float)Math.Sin(angle * i)); //y;
            Vertices[3 * (i + 1) + 2] = Origin.Z;                       //z;

            Triangles[3 * i] =      0;
            Triangles[3 * i + 1] =  (uint)(i + 1);
            Triangles[3 * i + 2] =  (uint)(i + 2);

        }
        DrawLength = Triangles.Length - 3;
        
        if (gl) base.Draw(force:force, gl:gl);
    }

    public override void Render(int shader_handle)
    {
        if (!Drawn)
        {
            Draw();
        }
        base.Render(shader_handle);
    }


    public float GetRadius()
    {
        return Radius;
    }
    public void SetRadius(float _radius)
    {
        Radius = _radius;
    }
}
