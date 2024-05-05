using System.Numerics;
using OpenTK.Windowing.Common;

namespace WSGraphics.Window;

public static class Common
{
    public struct WindowInitSettings
    {
        public Vector2 Size = new Vector2(640, 480);
        public string Title = "dotnet opentk_imgui";
        public ViewLogic Logic;
        public int Montitor = 0;
        public WindowState WindowInitState = WindowState.Normal;

        public WindowInitSettings(ViewLogic _logic)
        {
            Logic = _logic;
        }
        public WindowInitSettings(Vector2 _size, string _title, ViewLogic _logic)
        {
            Logic = _logic;
            Size = _size;
            Title = _title;
        }
    }
}