using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

using System.Numerics;
using OpenTK.Mathematics;

using ShapeMath;

using WindowLogic;
using OpenTK_Builtin;

namespace Window
{
    public class ShapeLogic : Logic
    {

        List<Shape> Shapes;

        int ShaderHandle;

        bool StartOfLine = true;
        

        public ShapeLogic(List<Shape> _shapes)
        {
            this.Shapes = _shapes;
        }

        public override void OnUpdateFrame(FrameEventArgs args, KeyboardState key, MouseState mouse)
        {
            return;
            // float norm_x = (mouse.Position.X / base.Size.X * 2) - 1.0f;
            // float norm_y = (mouse.Position.Y / base.Size.X * 2) - 1.0f;
            // norm_y *= -1.0f;

            // if (!this.StartOfLine)
            // {
                
            //     if (this.Shapes.ElementAt(this.Shapes.Count-1).GetType() == typeof(Line))
            //     {
                  
            //         ((Line)this.Shapes.ElementAt(this.Shapes.Count-1)).SetPointB(new Vector2(norm_x, norm_y));
            //         ((Line)this.Shapes.ElementAt(this.Shapes.Count-1)).Draw(force:true);
            //     }
            // }


            // if (mouse.IsButtonPressed(MouseButton.Left))
            // {
            //     Circle c = new Circle(new Vector2(norm_x, norm_y), 0.03f);
            //     c.Wireframe = true;
            //     Shapes.Add(c);
                
            //     if (this.StartOfLine)
            //     {
            //         Console.WriteLine("Start");
            //         this.StartOfLine = false;
            //         Line l1 = new Line(
            //             new Vector2(norm_x, norm_y), 
            //             new Vector2(norm_x, norm_y),
            //             new Vector2(norm_x+0.0001f, norm_y+0.0001f), 0.01f
            //         );
            //         l1.Wireframe = true;
            //         this.Shapes.Add(l1);
            //     }
            //     else
            //     {
            //         Console.WriteLine("End");
            //         this.StartOfLine = true;
            //     }
                    
            // }
            
        }

        public override void OnRenderFrame(FrameEventArgs args, Camera camera, OpenTK.Mathematics.Vector2 modelRotation)
        {
            // Use the shader
            GL.UseProgram(this.ShaderHandle);

            float x_rotation = MathHelper.DegreesToRadians(modelRotation.X);
            float y_rotation = MathHelper.DegreesToRadians(modelRotation.Y);

            Matrix4 model = Matrix4.CreateRotationX(x_rotation);
            model *= Matrix4.CreateRotationY(y_rotation);


            this.SetMatrix4("model", model);
            this.SetMatrix4("view", camera.GetViewMatrix());
            this.SetMatrix4("projection", camera.GetProjectionMatrix());
          

            foreach(Shape shape in Shapes)
            {
                shape.Render(this.ShaderHandle);
            }
            // // Set wireframe
            // GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

        }

        private Dictionary<string, int>? _uniformLocations;

        public void SetMatrix4(string name, Matrix4 data)
        {
            if (_uniformLocations != null)
            {
                if (_uniformLocations.ContainsKey(name))
                {
                    GL.UseProgram(ShaderHandle);
                GL.UniformMatrix4(_uniformLocations[name], true, ref data);
                }
            }
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

             GL.GetProgram(ShaderHandle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

            // Next, allocate the dictionary to hold the locations.
            _uniformLocations = new Dictionary<string, int>();

            // Loop over all the uniforms,
            for (var i = 0; i < numberOfUniforms; i++)
            {
                // get the name of this uniform,
                var key = GL.GetActiveUniform(ShaderHandle, i, out _, out _);

                // get the location,
                var location = GL.GetUniformLocation(ShaderHandle, key);

                // and then add it to the dictionary.
                _uniformLocations.Add(key, location);
            }
        }

        static string vert = 
        """
            #version 330 core
            layout (location = 0) in vec3 aPosition;
            layout(location = 1) in vec2 aTexCoord;

            out vec2 texCoord;


            uniform mat4 model;
            uniform mat4 view;
            uniform mat4 projection;

            void main()
            {
                texCoord = aTexCoord;

                gl_Position = vec4(aPosition, 1.0) * model * view * projection;
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