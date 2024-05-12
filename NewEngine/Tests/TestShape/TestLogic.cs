using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;
using WSGraphics.Window;
using OpenTK.Mathematics;
using System.Diagnostics;
using System.Collections.Generic;

using WSGraphics.Graphics;
using WSGraphics.Graphics.Geometry;

namespace Program;

class TestLogic : ViewLogic
{
    public static float[] Vertices =
    {
        -0.433f, -0.25f, 0.0f, // Bottom-left vertex
        0.433f, -0.25f, 0.0f, // Bottom-right vertex
        0.0f,  0.5f, 0.0f  // Top vertex
    };

    public static System.Numerics.Vector4 ColorPicked = new System.Numerics.Vector4(1.0f, 1.0f, 1.0f, 1.0f);

    private int VertexBufferObject;
    private int VertexArrayObject;

    private Shader MyShader;

    Stopwatch MyTimer;
    Stopwatch RenderTimer;

    // Circle Circ = new Circle();
    public static System.Numerics.Vector3 CircOrigin = new System.Numerics.Vector3(0.0f);
    static List<Shape> Shapes = new List<Shape>();



    Camera MainCamera;

    public float StartZ = 1.15f;
    public OpenTK.Mathematics.Vector2 ModelRotation = new OpenTK.Mathematics.Vector2(0.0f, 0.0f);

    bool MiddleMouse = false;
    private bool _firstMove = true;
    private OpenTK.Mathematics.Vector2 _lastPos;

    float RotationSensitivity = 0.6f;

    const float cameraSpeed = 0.05f;
    private float LastScroll = 0;




    public TestLogic(Shader _shader)
    {
        MyShader = _shader;
        MainCamera = new Camera(OpenTK.Mathematics.Vector3.UnitZ * StartZ, 1.0f);
        

        MyTimer = new Stopwatch();
        MyTimer.Start();

        RenderTimer = new Stopwatch();
    }

    public override void DoLoad()
    {

        VertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

        
        GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);


        VertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(VertexArrayObject);

    
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

        // Enable variable 0 in the shader.
        GL.EnableVertexAttribArray(0);

    

        

        MyShader.Load();
        MyShader.Use();

        GenCirc(ref ShapesBuffer);
        CopyShapesBuffer();
    }

    public static float Mod = 0.61f;
    public static float Bias = -0.23f;

    public override void DoRenderFrame(FrameEventArgs args, Camera? camera, Vector2? modelRotation)
    {
        RenderTimer.Restart();
        RenderTimer.Start();
        MyShader.Use();

        // GL.DeleteVertexArray(VertexArrayObject);

        // VertexArrayObject = GL.GenVertexArray();
        // GL.BindVertexArray(VertexArrayObject);

        // GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        // GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);
        // // 3. then set our vertex attributes pointers
        // GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        // GL.EnableVertexAttribArray(0);

        // double timeValue = MyTimer.Elapsed.TotalMilliseconds / (2000 * (1.01-Mod));
        // float mod = (float)Math.Sin(timeValue) / 2.0f + (0.5f + Bias);

        // int vertexColorLocation = GL.GetUniformLocation(MyShader.Handle, "vertexColor");
        //         GL.Uniform4(vertexColorLocation, 
        //             ColorPicked.X * mod,
        //             ColorPicked.Y * mod,
        //             ColorPicked.Z * mod,
        //             ColorPicked.W * mod);

        // GL.BindVertexArray(VertexArrayObject);
        // GL.DrawArrays(PrimitiveType.Triangles, 0, 3);


        MainCamera.AspectRatio = Size.X / (float)Size.Y;
        float x_rotation = MathHelper.DegreesToRadians(ModelRotation.X);
        float y_rotation = MathHelper.DegreesToRadians(ModelRotation.Y);

        Matrix4 model = Matrix4.CreateRotationX(x_rotation);
        model *= Matrix4.CreateRotationY(y_rotation);
        

        MyShader.SetMatrix4("model", model);
        MyShader.SetMatrix4("view", MainCamera.GetViewMatrix());
        MyShader.SetMatrix4("projection", MainCamera.GetProjectionMatrix());

        // Console.WriteLine("Model");
        // Console.WriteLine(model);
        // Console.WriteLine("View");
        // Console.WriteLine(MainCamera.GetViewMatrix());
        // Console.WriteLine("Projection");
        // Console.WriteLine(MainCamera.GetProjectionMatrix());

        // double timeValue = MyTimer.Elapsed.TotalMilliseconds;

        // if (timeValue - lastTime > between)
        // {
        //     lastTime = timeValue;

        
        
  
        if (!CircRunning)
        {
            // Console.WriteLine("**Start thread");
            _time = time += (float)between * Mod;
            CircThread = new Thread(() => GenCirc(ref ShapesBuffer));
            CircRunning = true;
            CircThread.Start();
        }
        else
        {
            if (CircThread.IsAlive)
            {
                // Console.WriteLine("**Thread running");
            }
            else
            {
                // Console.WriteLine("**Thread finished");

                CircRunning = false;
            }
        }

        if (ShapeBufferFilled && !CircRunning)
        {
            // Console.WriteLine("**Copy shapes");
            ShapeBufferFilled = false;
            CopyShapesBuffer();
        }
        

        


        
            

            

        // }
        
        
        // Console.WriteLine("**Render");
      
        foreach (Shape shape in Shapes)
        {
           
            shape.Render(MyShader.Handle);
        }
        ObjectsRendered = Shapes.Count;
        

        RenderTimer.Stop();
        
        avgTime.Enqueue(RenderTimer.ElapsedMilliseconds);

        if (avgTime.Count > 100)
        {
            avgTime.Dequeue();
        }

        RenderProcessing = avgTime.Sum() / (float)avgTime.Count;
        

    }

    public static int ObjectsRendered = 0;
    public static float RenderProcessing = 0.0f;



    double lastTime = 0.0f;
    double between = 10.0f;

    float time = 0.0f;

    public static float CircSize = 0.001f;


    static List<Shape> ShapesBuffer = new List<Shape>();
    static bool ShapeBufferFilled = false;
    static bool CircRunning = false;
    Thread CircThread = new Thread(() => GenCirc(ref ShapesBuffer));

    static float _time = 0.0f;
    public static float mult = 92.0f;
    public static float mMult = 0.0f;

    private static void GenCirc(ref List<Shape> _shapesBuffer)
    {
        ShapeBufferFilled = false;
        _shapesBuffer = new List<Shape>();

        float m = 10.0f;

        float inc = 0.01f;

        
        float add = _time;

        float golden = (float)(1 + Math.Pow(5, 0.5f));


        System.Numerics.Vector3 lastPosition = new System.Numerics.Vector3(0.0f);

        for (float t = 0.0f; t < m; t+=inc)
        // Parallel.For(0.0f, m, t =>
        {
            float x = (float)(Math.Sin(t * (mult-mult*Math.Sin(_time*mMult)) + add) * Math.Pow(golden, t / Math.PI));
            float y = (float)(Math.Cos(t * (mult-mult*Math.Sin(_time*mMult)) + add) * Math.Pow(golden, t / Math.PI));
            float z = -0.5f + t  / m * m;

            x /= m;
            y /= m;
            z /= m;

            Circle circ = new Circle(new System.Numerics.Vector3(x, y, z), CircSize, _segments:64);
            // circ.Wireframe = true;
            circ.SetColor(ColorPicked);
            // circ.Draw(force:true);

            _shapesBuffer.Add(circ);


            if (t > 0.0f)
            {
                Line lin = new Line(System.Numerics.Vector3.Zero, lastPosition, new System.Numerics.Vector3(x, y, z), CircSize);
                lin.SetColor(ColorPicked);
                // lin.Draw(force:true);

                _shapesBuffer.Add(lin);
            }
            lastPosition.X = x;
            lastPosition.Y = y;
            lastPosition.Z = z;
        }
        ShapeBufferFilled = true;

    }

    private static void CopyShapesBuffer()
    {
        foreach (Shape shape in Shapes)
        // Parallel.For(0, Shapes.Count, i =>
        {
            shape.Dispose();
        }

        Shapes = new List<Shape>();

        foreach (Shape shape in ShapesBuffer)
        // Parallel.For(0, ShapesBuffer.Count, i =>
        {
            shape.Draw(force:true);
            Shapes.Add(shape);
        }
        ShapesBuffer = new List<Shape>();
    }

    Queue<float> avgTime = new Queue<float>();



    public override void DoUpdateFrame(FrameEventArgs args, KeyboardState key, MouseState mouse)
    {

        if (key.IsKeyDown(Keys.Escape))
        {
            // Close();
            Environment.Exit(0);
        }

        // if (!IsFocused) // Check to see if the window is focused
        // {
        //     return;
        // }

        if (key.IsKeyPressed(Keys.F))
        {
            MainCamera = new Camera(OpenTK.Mathematics.Vector3.UnitZ * StartZ, Size.X / (float)Size.Y);
            ModelRotation = new OpenTK.Mathematics.Vector2(0.0f, 0.0f);
        }

        bool rotate = mouse[MouseButton.Middle] && !key.IsKeyDown(Keys.LeftShift);
        bool pan = mouse[MouseButton.Middle] && key.IsKeyDown(Keys.LeftShift);

        if (pan)
        {
            if (!this.MiddleMouse)
            {
                this.MiddleMouse = true;
                _lastPos = new OpenTK.Mathematics.Vector2(mouse.X, mouse.Y);
            }
            else
            {
                float deltaX = mouse.X - _lastPos.X;
                float deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new OpenTK.Mathematics.Vector2(mouse.X, mouse.Y);

                float h = 2.0f / Size.X;
                float w = 2.0f / Size.Y;

                MainCamera.Position += deltaY * (MainCamera.Up * h);
                MainCamera.Position -= deltaX * (OpenTK.Mathematics.Vector3.Normalize(OpenTK.Mathematics.Vector3.Cross(MainCamera.Front,MainCamera.Up)) * w);
            }

        }
        else
        {
            this.MiddleMouse = false;
        }


        if (rotate)
        {
            if (_firstMove) // this bool variable is initially set to true
            {
                _lastPos = new OpenTK.Mathematics.Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }
            else
            {
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new OpenTK.Mathematics.Vector2(mouse.X, mouse.Y);

                
                this.ModelRotation.X += deltaY * RotationSensitivity; 
                this.ModelRotation.Y += deltaX * RotationSensitivity;
            }
        }
        else
        {
            _firstMove = true;
        }

        float current_scroll = mouse.Scroll.Y;
        float diff = current_scroll - LastScroll;
        
        if (diff == 1)
        {
            MainCamera.Position += MainCamera.Front * cameraSpeed; // Forward
        }
        if (diff == -1)
        {
            MainCamera.Position -= MainCamera.Front * cameraSpeed; // Backwards
        }
        LastScroll = current_scroll;
    }
}
