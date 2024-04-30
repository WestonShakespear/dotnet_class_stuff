using ShapeLib;
using Window;
using System.Numerics;
using ObjLoader.Loader.Loaders;
using System.Security.Principal;

namespace Test
{
    public static class Test
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("");

            List<Shape> shapes = new List<Shape>();

            


            // Input
            Vector2 initialP = new Vector2(0.0f, 0.0f);

            // commandX, commandY, I, J

            // List<Vector4> inputs = new List<Vector4>()
            // {
                
            //     // new Vector4( 0.0001f, 1.0f,  2.0f,  2.0f),


            //     // Quadrant 1 ++
            //     new Vector4( 1.0f, -2.0f,  2.0f,  2.0f),//**
            //     new Vector4( 4.0f, -2.0f,  2.0f,  2.0f),
            //     new Vector4( 6.0f,  1.0f,  2.0f,  2.0f),
            //     new Vector4( 6.0f,  4.0f,  2.0f,  2.0f),
            //     new Vector4(-1.0f,  6.0f,  2.0f,  2.0f),
            //     new Vector4(-2.0f,  1.0f,  2.0f,  2.0f),

            //     // Quadrant 2 -+
            //     new Vector4(-1.0f, -2.0f, -2.0f,  2.0f),//**
            //     new Vector4(-4.0f, -2.0f, -2.0f,  2.0f),
            //     new Vector4(-6.0f,  1.0f, -2.0f,  2.0f),
            //     new Vector4(-6.0f,  4.0f, -2.0f,  2.0f),
            //     new Vector4( 1.0f,  6.0f, -2.0f,  2.0f),
            //     new Vector4( 2.0f,  1.0f, -2.0f,  2.0f),

            //     // Quadrant 3 --
            //     new Vector4(-1.0f,  2.0f, -2.0f, -2.0f),//**
            //     new Vector4(-4.0f,  2.0f, -2.0f, -2.0f),
            //     new Vector4(-6.0f, -1.0f, -2.0f, -2.0f),
            //     new Vector4(-6.0f, -4.0f, -2.0f, -2.0f),
            //     new Vector4( 1.0f, -6.0f, -2.0f, -2.0f),
            //     new Vector4( 2.0f, -1.0f, -2.0f, -2.0f),

            //     // Quadrant 4 +-
            //     new Vector4( 1.0f,  2.0f,  2.0f, -2.0f),//**
            //     new Vector4( 4.0f,  2.0f,  2.0f, -2.0f),
            //     new Vector4( 6.0f, -1.0f,  2.0f, -2.0f),
            //     new Vector4( 6.0f, -4.0f,  2.0f, -2.0f),
            //     new Vector4(-1.0f, -6.0f,  2.0f, -2.0f),
            //     new Vector4(-2.0f, -1.0f,  2.0f, -2.0f),
            // };

            List<Vector4> inputs = new List<Vector4>()
            {
                
                // new Vector4( 0.0001f, 1.0f,  2.0f,  2.0f),


                // Quadrant 1 ++
                new Vector4( 2.0f, -2.0f,  2.0f,  2.0f),//**
                new Vector4( 5.0f,  2.0f,  2.0f,  2.0f),
                new Vector4( 6.0f,  1.0f,  2.0f,  2.0f),
                new Vector4( 6.0f,  4.0f,  2.0f,  2.0f),

                // Quadrant 2 -+
                new Vector4(-2.0f, -2.0f, -2.0f,  2.0f),//**
                new Vector4( 5.0f,  2.0f, -2.0f,  2.0f),
                new Vector4(-6.0f,  1.0f, -2.0f,  2.0f),
                new Vector4(-6.0f,  4.0f, -2.0f,  2.0f),

                // Quadrant 3 --
                new Vector4(-2.0f,  2.0f, -2.0f, -2.0f),//**
                new Vector4( 5.0f, -2.0f, -2.0f, -2.0f),
                new Vector4(-6.0f, -1.0f, -2.0f, -2.0f),
                new Vector4(-6.0f, -4.0f, -2.0f, -2.0f),

                // Quadrant 4 +-
                new Vector4( 2.0f,  2.0f,  2.0f, -2.0f),//**
                new Vector4( 5.0f, -2.0f,  2.0f, -2.0f),
                new Vector4( 6.0f, -1.0f,  2.0f, -2.0f),
                new Vector4( 6.0f, -4.0f,  2.0f, -2.0f),
            };

            int col = 4;
            float spacing = 15.0f;
            float scale = 20.0f;
            float pSize = 0.02f * 10.0f;
            float aSize = 0.01f * 10.0f;
            float angular = 2.0f;

            float colorPlus = 0.15f * (1/ (20.0f / angular));

            float originHome = (-1 * (float)(col/2) * spacing) + (spacing / 2);

            Vector2 origin = new Vector2(originHome, (inputs.Count / col / 2 * spacing) - (spacing / 2));

            int c = 1;


            //show arc points
            bool arcPointDraw = false;
            bool arcLineDraw = true;
            

            foreach (Vector4 vec in inputs)
            {
                ArcData arcData = new ArcData(initialP, new Vector2(vec.X, vec.Y), vec.Z, vec.W);
                ArcData arcDataCW = new ArcData(initialP, new Vector2(vec.X, vec.Y), vec.Z, vec.W);

                CalculateArc(ref arcData, _angularResolution:angular);
                CalculateArc(ref arcDataCW, _cw:false, _angularResolution:angular);



                //--------------------------------------------

                


                float color = 0.0f;

                int p = 0;
                Vector2 lastPoint = new Vector2();

                foreach (Vector2 point in arcData.StepPoints)
                {
                    Vector4 colorV = new Vector4(0.0f, color, 1.0f, 1.0f);
                    Vector2 outPoint = new Vector2((point.X+origin.X) / scale, (point.Y+origin.Y) / scale);

                    if (arcPointDraw)
                    {
                        Circle arcPoint = new Circle(outPoint, aSize / scale);
                        arcPoint.SetColor(colorV);
                        shapes.Add(arcPoint);
                    }

                    if (p > 0 && arcLineDraw)
                    {
                        Line cLine = new Line(new Vector2(), outPoint, lastPoint, aSize / scale / 4);

                        cLine.SetColor(colorV);
                        shapes.Add(cLine);
                    }

                    color += colorPlus;
                    lastPoint = outPoint;
                    p++;
                }

                Dictionary<Circle, Vector4> edgeCircles = new Dictionary<Circle, Vector4>()
                {
                    { new Circle(new Vector2((arcData.StartPoint.X+origin.X) / scale, (arcData.StartPoint.Y+origin.Y) / scale), pSize / scale), new Vector4(1.0f, 0.0f, 0.0f, 0.8f) },
                    { new Circle(new Vector2((arcData.CommandPoint.X+origin.X) / scale, (arcData.CommandPoint.Y+origin.Y) / scale), pSize / scale), new Vector4(0.0f, 0.0f, 0.0f, 0.8f) },
                    { new Circle(new Vector2((arcData.CenterPoint.X+origin.X) / scale, (arcData.CenterPoint.Y+origin.Y) / scale), pSize / scale),   new Vector4(0.0f, 0.0f, 1.0f, 0.8f) },
                    { new Circle(new Vector2((arcData.EndPoint.X+origin.X) / scale, (arcData.EndPoint.Y+origin.Y) / scale), pSize / scale),         new Vector4(0.0f, 1.0f, 0.0f, 0.8f) },
                };

                foreach (KeyValuePair<Circle, Vector4> circ in edgeCircles)
                {
                    circ.Key.SetColor(circ.Value);
                    shapes.Add(circ.Key);
                }


                color = 0.0f;
                p = 0;
                foreach (Vector2 point in arcDataCW.StepPoints)
                {
                    Vector4 colorV = new Vector4(1.0f, 0.0f, color, 1.0f);
                    Vector2 outPoint = new Vector2((point.X+origin.X) / scale, (point.Y+origin.Y) / scale);

                    if (arcPointDraw)
                    {
                        Circle arcPoint = new Circle(outPoint, aSize / scale);
                        arcPoint.SetColor(colorV);
                        shapes.Add(arcPoint);
                    }

     

                    if (p > 0 && arcLineDraw)
                    {
                        Line cLine = new Line(new Vector2(), outPoint, lastPoint, aSize / scale / 4);

                        cLine.SetColor(colorV);
                        shapes.Add(cLine);
                    }

                    color += colorPlus;
                    lastPoint = outPoint;
                    p++;
                }

                Dictionary<Circle, Vector4> edgeCirclesCW = new Dictionary<Circle, Vector4>()
                {
                    { new Circle(new Vector2((arcDataCW.StartPoint.X+origin.X) / scale, (arcDataCW.StartPoint.Y+origin.Y) / scale), pSize / scale), new Vector4(1.0f, 0.0f, 0.0f, 0.8f) },
                    { new Circle(new Vector2((arcDataCW.CommandPoint.X+origin.X) / scale, (arcDataCW.CommandPoint.Y+origin.Y) / scale), pSize / scale), new Vector4(1.0f, 1.0f, 0.0f, 0.8f) },
                    { new Circle(new Vector2((arcDataCW.CenterPoint.X+origin.X) / scale, (arcDataCW.CenterPoint.Y+origin.Y) / scale), pSize / scale),   new Vector4(0.0f, 0.0f, 1.0f, 0.8f) },
                    { new Circle(new Vector2((arcDataCW.EndPoint.X+origin.X) / scale, (arcDataCW.EndPoint.Y+origin.Y) / scale), pSize / scale),         new Vector4(0.0f, 1.0f, 0.0f, 0.8f) },
                };

                foreach (KeyValuePair<Circle, Vector4> circ in edgeCirclesCW)
                {
                    circ.Key.SetColor(circ.Value);
                    shapes.Add(circ.Key);
                }
               
                //-----------------------------------

                origin.X += spacing;

                if (c++ % col == 0) { origin.X = originHome; origin.Y -= spacing; Console.WriteLine();}
            }
//---------------------------------------------------------------



            

           


            

            


            




            



            using (
                TestWindow.CameraWindow game = new TestWindow.CameraWindow(
                    1920, 1080, "HelloTriangle",
                    new OBJ_Logic(shapes), fullscreen:true)
                )
            {
                game.Run();
            }
     
        }

        const float FULLC = (float) (2 * Math.PI);

        // public static void CalculateArc(ref ArcData _arc, bool _cw = true, float _angularResolution = 10.0f)
        // {
            
        //     int pointCount = (int)(360 / _angularResolution);

        //     float radius = (float) Math.Sqrt(
        //         (_arc.XOffset*_arc.XOffset) + (_arc.YOffset*_arc.YOffset) );
        //     float startAngle = 0.0f;
            

        //     bool radiusXLarger = _arc.CenterPoint.X > _arc.StartPoint.X;
        //     bool radiusYLarger = _arc.CenterPoint.Y > _arc.StartPoint.Y;

        //     if (radiusXLarger)
                
        //         startAngle = (float) (-1 * Math.PI + Math.Asin( _arc.YOffset / radius ));

        //     else if (!radiusXLarger && radiusYLarger)
                
        //         startAngle = (float) (-1 * Math.PI / 2 - Math.Asin( _arc.XOffset / radius ));

        //     else if (!radiusXLarger && !radiusYLarger)
                
        //         startAngle = (float) (-1 * Math.Asin( _arc.YOffset / radius ));
            

        //     float travelAngle = (float) (-1 * startAngle + Math.Atan( (_arc.CommandPoint.Y - _arc.CenterPoint.Y) / (_arc.CommandPoint.X - _arc.CenterPoint.X) ));
            

        //     if (_arc.CommandPoint.X < _arc.CenterPoint.X)     travelAngle += (float) Math.PI; 


        //     while (startAngle < 0) startAngle += FULLC;
        //     while (startAngle > 2 * Math.PI) startAngle -= FULLC;

        //     while (travelAngle < 0) travelAngle += FULLC;
        //     while (travelAngle > 2 * Math.PI) travelAngle -= FULLC;


        //     _arc.EndPoint.X = (float) (_arc.CenterPoint.X + radius * Math.Cos(startAngle - travelAngle));
        //     _arc.EndPoint.Y = (float) (_arc.CenterPoint.Y + radius * Math.Sin(startAngle - travelAngle));


        //     float start = 0.0f;
        //     float cirPer = (float) (travelAngle / 2 * Math.PI) / 10;

        //     pointCount = (int)(pointCount * cirPer);
        //     if (pointCount < 2) pointCount = 2;

        //     float iter = (travelAngle - start) / pointCount;
      
      
        //     for (int i = 0; i <= pointCount; i++)
        //     {
        //         float oX = (float) ( _arc.CenterPoint.X + radius * Math.Cos(startAngle + (-1*i * iter)));
        //         float oY = (float) ( _arc.CenterPoint.Y + radius * Math.Sin(startAngle + (-1*i * iter)));

        //         _arc.StepPoints.Add(new Vector2(oX, oY));
        //     } 

        //     string mRad = startAngle.ToString("n2").PadLeft(8);
        //     string mDeg = (startAngle*180/Math.PI).ToString("n2").PadLeft(8);

        //     string nRad = travelAngle.ToString("n2").PadLeft(8);
        //     string nDeg = (travelAngle*180/Math.PI).ToString("n2").PadLeft(8);

        //     string segS = pointCount.ToString().PadLeft(4);

        //     Console.WriteLine("| M: \t( {0}rad )\t ( {1}deg ) | N:  \t( {2}rad )\t ( {3}deg ) | Seg: {4} | Perc: {5} |",
        //         mRad, mDeg,
        //         nRad, nDeg,
        //         segS,
        //         cirPer.ToString().PadLeft(8));
        // }

        public struct ArcData
        {
            public Vector2 StartPoint;
            public Vector2 EndPoint;
            public Vector2 CommandPoint;
            public Vector2 CenterPoint;

            public List<Vector2> StepPoints;

            public float XOffset;
            public float YOffset;

            public ArcData(Vector2 _startPoint, Vector2 _commandPoint, float _xOffset, float _yOffset)
            {
                XOffset = _xOffset;
                YOffset = _yOffset;
                StartPoint = _startPoint;
                EndPoint = new Vector2();
                CommandPoint = _commandPoint;
                CenterPoint = new Vector2(StartPoint.X + XOffset, StartPoint.Y + YOffset);

                StepPoints = new List<Vector2>();
            }
        }





        public static void CalculateArc(ref ArcData _arc, bool _cw = true, float _angularResolution = 10.0f)
        {
            
            int pointCount = (int)(360 / _angularResolution);

            float radius = (float) Math.Sqrt(
                (_arc.XOffset*_arc.XOffset) + (_arc.YOffset*_arc.YOffset) );
            float startAngle = 0.0f;
            

            bool radiusXLarger = _arc.CenterPoint.X > _arc.StartPoint.X;
            bool radiusYLarger = _arc.CenterPoint.Y > _arc.StartPoint.Y;

            if (radiusXLarger)
                
                startAngle = (float) (-1 * Math.PI + Math.Asin( _arc.YOffset / radius ));

            else if (!radiusXLarger && radiusYLarger)
                
                startAngle = (float) (-1 * Math.PI / 2 - Math.Asin( _arc.XOffset / radius ));

            else if (!radiusXLarger && !radiusYLarger)
                
                startAngle = (float) (-1 * Math.Asin( _arc.YOffset / radius ));
            

            float travelAngle = (float) (-1 * startAngle + Math.Atan( (_arc.CommandPoint.Y - _arc.CenterPoint.Y) / (_arc.CommandPoint.X - _arc.CenterPoint.X) ));

            if (_cw)    travelAngle = (float) ((2*Math.PI) - travelAngle);
            if (_arc.CommandPoint.X < _arc.CenterPoint.X)     travelAngle += (float) Math.PI; 


            while (startAngle < 0) startAngle += FULLC;
            while (startAngle > 2 * Math.PI) startAngle -= FULLC;

            while (travelAngle < 0) travelAngle += FULLC;
            while (travelAngle > 2 * Math.PI) travelAngle -= FULLC;


            float endAngle = startAngle + travelAngle;
            if (_cw) endAngle = startAngle - travelAngle;

            _arc.EndPoint.X = (float) (_arc.CenterPoint.X + radius * Math.Cos(endAngle));
            _arc.EndPoint.Y = (float) (_arc.CenterPoint.Y + radius * Math.Sin(endAngle));


            float cirPer = (float) (travelAngle / 2 * Math.PI) / 10;

            pointCount = (int)(pointCount * cirPer);
            if (pointCount < 2) pointCount = 2;

            float iter = travelAngle / pointCount;
      
      
            for (int i = 0; i <= pointCount; i++)
            {
                float moveAngle = startAngle + i * iter;
                if (_cw) moveAngle = startAngle - i * iter;

                float oX = (float) ( _arc.CenterPoint.X + radius * Math.Cos(moveAngle));
                float oY = (float) ( _arc.CenterPoint.Y + radius * Math.Sin(moveAngle));

                _arc.StepPoints.Add(new Vector2(oX, oY));
            } 

            string mRad = startAngle.ToString("n2").PadLeft(8);
            string mDeg = (startAngle*180/Math.PI).ToString("n2").PadLeft(8);

            string nRad = travelAngle.ToString("n2").PadLeft(8);
            string nDeg = (travelAngle*180/Math.PI).ToString("n2").PadLeft(8);

            string segS = pointCount.ToString().PadLeft(4);

            Console.WriteLine("| M: \t( {0}rad )\t ( {1}deg ) | N:  \t( {2}rad )\t ( {3}deg ) | Seg: {4} | Perc: {5} |",
                mRad, mDeg,
                nRad, nDeg,
                segS,
                cirPer.ToString().PadLeft(8));
        }





        
    }
}