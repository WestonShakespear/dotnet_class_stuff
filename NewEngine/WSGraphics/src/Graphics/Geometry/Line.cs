using System.Numerics;

namespace WSGraphics.Graphics.Geometry;

public class Line : Shape
{
    private Vector3 PointA;
    private Vector3 PointB;
    private float Width;

    public Line()
    {
        Draw(gl:false);
    }
    public Line(Vector2 _origin, Vector2 _pointA, Vector2 _pointB, float _width)
    {
        Origin = new Vector3(_origin.X, _origin.Y, 0.0f);
        PointA = new Vector3(_pointA.X, _pointA.Y, 0.0f);
        PointB = new Vector3(_pointB.X, _pointB.Y, 0.0f);
        Width = _width;

        Draw(gl:false);
    }
    public Line(Vector3 _origin, Vector3 _pointA, Vector3 _pointB, float _width)
    {
        Origin = new Vector3(_origin.X, _origin.Y, 0.0f);
        PointA = _pointA;
        PointB = _pointB;
        Width = _width;

        Draw(gl:false);
    }


    public override void Draw(bool force = false, bool gl = true)
    {
        bool swap = false;

        if (PointB.Y < PointA.Y) swap = true;

        if ( (PointA.Y == PointB.Y) && PointB.X < PointA.X) swap = true;

        if (swap)
        {
            Vector3 cl = new Vector3(PointA.X, PointA.Y, PointA.Z);
            PointA = new Vector3(PointB.X, PointB.Y, PointB.Z);
            PointB = new Vector3(cl.X, cl.Y, cl.Z);
        }

            
        double length = Math.Sqrt(Math.Pow(PointB.X - PointA.X, 2) + Math.Pow(PointB.Y - PointA.Y, 2));
        double angle = Math.Atan( (PointB.Y - PointA.Y) / (PointB.X - PointA.X) ) ;
        double posX_length = Width * Math.Cos(angle + Math.PI/2);
        double posY_length = Width * Math.Sin(angle + Math.PI/2);
        double negX_length = Width * Math.Cos(angle - Math.PI/2);
        double negY_length = Width * Math.Sin(angle - Math.PI/2);

        // Console.WriteLine("    Angle: {0}", angle * 180/Math.PI);

        if (angle < 0) angle += (float)Math.PI;


        Vertices = new float[]
        {
            (float)(PointA.X + posX_length), (float)(PointA.Y + posY_length), PointA.Z,
            (float)(PointA.X + negX_length), (float)(PointA.Y + negY_length), PointA.Z,
            
            (float)(PointA.X + length * Math.Cos(angle) + posX_length),
            (float)(PointA.Y + length * Math.Sin(angle) + posY_length),
            PointB.Z,

            (float)(PointA.X + length * Math.Cos(angle) + negX_length),
            (float)(PointA.Y + length * Math.Sin(angle) + negY_length),
            PointB.Z
        };

        Triangles = new uint[]
        {
            0, 1, 2,
            1, 2, 3
        };
        
        DrawLength = Triangles.Length;
        
        if (gl) base.Draw(force:force);
    }

    public override void Render(int shader_handle)
    {
        if (!Drawn)
        {
            Draw();
        }
        base.Render(shader_handle);
    }

    public Vector3 GetPointA()
    {
        return PointA;
    }
    public void SetPointA(Vector3 _pointA)
    {
        PointA = _pointA;
    }

    public Vector3 GetPointB()
    {
        return PointB;
    }
    public void SetPointB(Vector3 _pointB)
    {
        PointB = _pointB;
    }


    
}
