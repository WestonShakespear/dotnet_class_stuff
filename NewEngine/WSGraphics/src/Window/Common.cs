using System.Numerics;
using OpenTK.Windowing.Common;

using WSGraphics.GUI;

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

        public Vector4 ClearColor = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
        public string FontPath = @"C:\Windows\Fonts\calibri.ttf";
        public float FontSize = 25f;

        public WindowInitSettings(ViewLogic _logic)
        {
            Logic = _logic;
        }
    }
}