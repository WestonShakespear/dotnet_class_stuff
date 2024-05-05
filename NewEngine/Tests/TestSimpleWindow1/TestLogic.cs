using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;
using WSGraphics.Window;
using OpenTK.Mathematics;


namespace Program;

class TestLogic : ViewLogic
{
    private readonly float[] _vertices =
    {
        -0.5f, -0.5f, 0.0f, // Bottom-left vertex
        0.5f, -0.5f, 0.0f, // Bottom-right vertex
        0.0f,  0.5f, 0.0f  // Top vertex
    };

    private int _vertexBufferObject;
    private int _vertexArrayObject;

    private TestShader _shader;

    public override void OnLoad()
    {

         _vertexBufferObject = GL.GenBuffer();

   
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

        
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);


        _vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(_vertexArrayObject);

    
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

        // Enable variable 0 in the shader.
        GL.EnableVertexAttribArray(0);

    
        _shader = new TestShader(@"C:\Users\Initec\github-repos\dotnet_class_stuff\NewEngine\Tests\TestSimpleWindow1\shader.vert", @"C:\Users\Initec\github-repos\dotnet_class_stuff\NewEngine\Tests\TestSimpleWindow1\shader.frag");


        _shader.Use();
    }

    public override void OnRenderFrame(FrameEventArgs args, Camera? camera, Vector2? modelRotation)
    {
        _shader.Use();

        GL.BindVertexArray(_vertexArrayObject);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

    }
}
