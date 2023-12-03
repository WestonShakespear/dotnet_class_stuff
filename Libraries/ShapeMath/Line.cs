using System.Numerics;

namespace ShapeMath
{
    public class Line : Shape
    {
        private Vector3 PointA;
        private Vector3 PointB;
        private float Width;

        public Line()
        {
            

            this.Draw(gl:false);
        }
        public Line(Vector2 _origin, Vector2 _pointA, Vector2 _pointB, float _width)
        {
            base.Origin = new Vector3(_origin.X, _origin.Y, 0.0f);
            this.PointA = new Vector3(_pointA.X, _pointA.Y, 0.0f);
            this.PointB = new Vector3(_pointB.X, _pointB.Y, 0.0f);
            this.Width = _width;

            this.Draw(gl:false);
        }
        public Line(Vector3 _origin, Vector3 _pointA, Vector3 _pointB, float _width)
        {
            base.Origin = new Vector3(_origin.X, _origin.Y, 0.0f);
            this.PointA = _pointA;
            this.PointB = _pointB;
            this.Width = _width;

            this.Draw(gl:false);
        }


        public override void Draw(bool force = false, bool gl = true)
        {
            bool swap = false;

            if (this.PointB.Y < this.PointA.Y) swap = true;

            if ( (this.PointA.Y == this.PointB.Y) && this.PointB.X < this.PointA.X) swap = true;

            if (swap)
            {
                Vector3 cl = new Vector3(this.PointA.X, this.PointA.Y, this.PointA.Z);
                this.PointA = new Vector3(this.PointB.X, this.PointB.Y, this.PointB.Z);
                this.PointB = new Vector3(cl.X, cl.Y, cl.Z);
            }

                
            double length = Math.Sqrt(Math.Pow(this.PointB.X - this.PointA.X, 2) + Math.Pow(this.PointB.Y - this.PointA.Y, 2));
            double angle = Math.Atan( (this.PointB.Y - this.PointA.Y) / (this.PointB.X - this.PointA.X) ) ;
            double posX_length = this.Width * Math.Cos(angle + Math.PI/2);
            double posY_length = this.Width * Math.Sin(angle + Math.PI/2);
            double negX_length = this.Width * Math.Cos(angle - Math.PI/2);
            double negY_length = this.Width * Math.Sin(angle - Math.PI/2);

            // Console.WriteLine("    Angle: {0}", angle * 180/Math.PI);

            if (angle < 0) angle += (float)Math.PI;


            base.Vertices = new float[]
            {
                (float)(this.PointA.X + posX_length), (float)(this.PointA.Y + posY_length), this.PointA.Z,
                (float)(this.PointA.X + negX_length), (float)(this.PointA.Y + negY_length), this.PointA.Z,
                
                (float)(this.PointA.X + length * Math.Cos(angle) + posX_length),
                (float)(this.PointA.Y + length * Math.Sin(angle) + posY_length),
                this.PointB.Z,

                (float)(this.PointA.X + length * Math.Cos(angle) + negX_length),
                (float)(this.PointA.Y + length * Math.Sin(angle) + negY_length),
                this.PointB.Z
            };

            base.Triangles = new uint[]
            {
                0, 1, 2,
                1, 2, 3
            };
            
            base.DrawLength = Triangles.Length;
            
            if (gl) base.Draw(force:force);
        }

        public override void Render(int shader_handle)
        {
            if (!base.Drawn)
            {
                this.Draw();
            }
            base.Render(shader_handle);
        }

        public Vector3 GetPointA()
        {
            return this.PointA;
        }
        public void SetPointA(Vector3 _pointA)
        {
            this.PointA = _pointA;
        }

        public Vector3 GetPointB()
        {
            return this.PointB;
        }
        public void SetPointB(Vector3 _pointB)
        {
            this.PointB = _pointB;
        }


        
    }
}