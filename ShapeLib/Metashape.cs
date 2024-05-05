using System.Numerics;

namespace ShapeLib
{
    public class Arc : Shape
    {
        private List<Shape> Shapes = new List<Shape>();

        public Arc()
        {
            this.Draw(gl:false);
        }

        public Arc(Vector3 _origin, List<Shape> _shapes)
        {
            this.Origin = _origin;
            this.Shapes = _shapes;

            this.Draw(gl:false);
        }


        public override void Draw(bool force = false, bool gl = true)
        {
            if (this.Drawn && !force) return;

            foreach (Shape shape in Shapes)
            {
                shape.Draw(force:force, gl:gl);
            }
        }

        public override void Render(int shader_handle)
        {
            if (!this.Display) return;

            foreach (Shape shape in Shapes)
            {
                shape.Display = this.Display;
                shape.Render(shader_handle);
            }
        }
        
    }
}