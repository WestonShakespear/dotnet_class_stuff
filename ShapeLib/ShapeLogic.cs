using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ShapeLib;
using System.Numerics;

namespace Window
{
    public class ShapeLogic : Logic
    {

        List<Shape> Shapes;

        int ShaderHandle;

        public ShapeLogic(List<Shape> _shapes, Vector2 Size)
        {
            this.Shapes = _shapes;
        }

        public override void OnUpdateFrame(FrameEventArgs args, KeyboardState key, MouseState mouse)
        {
            if (mouse.IsButtonPressed(MouseButton.Left))
            {
                Console.Write("Mouse pressed");
                Console.Write("    X: {0}", mouse.Position.X);
                Console.Write("    Y: {0}", mouse.Position.Y);
                float norm_x = (mouse.Position.X / ;
                float norm_y = mouse.Position.Y;

                Shapes.Add(new Circle(new Vector2(norm_x, norm_y), 0.05f));
            }
        }

        public override void OnRenderFrame(FrameEventArgs args)
        {
            // Use the shader
            GL.UseProgram(this.ShaderHandle);

            foreach(Shape shape in Shapes)
            {
                shape.Render(this.ShaderHandle);
            }
            // // Set wireframe
            // GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

        }
        
        public override void OnLoad()
        {
            foreach(Shape shape in Shapes)
            {
                shape.Draw();
            }
            this.LoadShader();
        }

        public override void OnUnload()
        {
            GL.DeleteProgram(this.ShaderHandle);
        }

        public override void OnResize()
        {

        }









        private void LoadShader()
        {
             //-----------------------LOAD THE SHADER----------------------//
            Console.WriteLine("LOAD THE SHADER");

            // Create the shaders
            int VertShader = GL.CreateShader(ShaderType.VertexShader);
            int FragShader = GL.CreateShader(ShaderType.FragmentShader);

            // Bind the code to them
            GL.ShaderSource(VertShader, vert);
            GL.ShaderSource(FragShader, frag);

            // Compile the shaders
            GL.CompileShader(VertShader);
            GL.CompileShader(FragShader);

            // Get the shader status
            GL.GetShader(VertShader, ShaderParameter.CompileStatus, out int vert_success);
            GL.GetShader(FragShader, ShaderParameter.CompileStatus, out int frag_success);

            Console.WriteLine("Vert succes: {0} Frag success: {1}", vert_success, frag_success);

            if (vert_success + frag_success != 2)
            {
                string info_vert = GL.GetShaderInfoLog(VertShader);
                string info_frag = GL.GetShaderInfoLog(FragShader);

                Console.WriteLine("Vertex Info: \r\n {0}", info_vert);
                Console.WriteLine();
                Console.WriteLine("Fragment Info: \r\n {0}", info_frag);
            }

            // Create the program
            this.ShaderHandle = GL.CreateProgram();

            // Attach shaders to program
            GL.AttachShader(this.ShaderHandle, VertShader);
            GL.AttachShader(this.ShaderHandle, FragShader);

            // Link the program and get the status
            GL.LinkProgram(this.ShaderHandle);

            GL.GetProgram(this.ShaderHandle, GetProgramParameterName.LinkStatus, out int success);

            if (success == 0)
            {
                Console.WriteLine("Shader Link: {0}", GL.GetProgramInfoLog(this.ShaderHandle));
            }

            // Now that it's stored we can remove
            GL.DetachShader(this.ShaderHandle, VertShader);
            GL.DetachShader(this.ShaderHandle, FragShader);
            GL.DeleteShader(VertShader);
            GL.DeleteShader(FragShader);
        }

        static string vert = 
        """
            #version 330 core
            layout (location = 0) in vec3 aPosition;

            void main()
            {
                gl_Position = vec4(aPosition.x, aPosition.y, aPosition.z, 1.0);
            }
        """;

        static string frag = 
        """
            #version 330 core
            out vec4 FragColor;
            uniform vec4 color;

            void main()
            {
                FragColor = color;
            }
        """;
    }
}