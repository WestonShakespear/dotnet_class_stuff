using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;
using WSGraphics.Window;
using OpenTK.Mathematics;
using System.Diagnostics;

using WSGraphics.Graphics;

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

    public TestLogic(Shader _shader)
    {
        MyShader = _shader;

        MyTimer = new Stopwatch();
        MyTimer.Start();
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
    }

    public static float Mod = 0.1f;
    public static float Bias = 0.0f;

    public override void DoRenderFrame(FrameEventArgs args, Camera? camera, Vector2? modelRotation)
    {
        MyShader.Use();

        GL.DeleteVertexArray(VertexArrayObject);

        VertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(VertexArrayObject);

        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);
        // 3. then set our vertex attributes pointers
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        double timeValue = MyTimer.Elapsed.TotalMilliseconds / (2000 * (1.01-Mod));
        float mod = (float)Math.Sin(timeValue) / 2.0f + (0.5f + Bias);

        int vertexColorLocation = GL.GetUniformLocation(MyShader.Handle, "vertexColor");
                GL.Uniform4(vertexColorLocation, 
                    ColorPicked.X * mod,
                    ColorPicked.Y * mod,
                    ColorPicked.Z * mod,
                    ColorPicked.W * mod);

        GL.BindVertexArray(VertexArrayObject);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

    }
}
