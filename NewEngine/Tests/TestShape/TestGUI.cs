using WSGraphics.GUI;
using OpenTK.Graphics.OpenGL4;
using ImGuiNET;
using static WSGraphics.Window.Common;

using static Program.TestLogic;
public class TestGUI : BaseGUI
{
    public static float fontSize = 0.6f;

    public static void RenderWindows()
    {
        LoadMenuBar();
        ValueWindow();
        InputWindow();
    }

       

    public static void LoadMenuBar()
    {
        ImGui.BeginMainMenuBar();
        if (ImGui.BeginMenu("File"))
        {
            if (ImGui.MenuItem("Save", "Ctrl + S")) 
            {
                Console.WriteLine("Saved");
            }
            ImGui.Separator();
            if (ImGui.MenuItem("Load"))
            {
                Console.WriteLine("Load");
            }

            ImGui.Separator();
            if (ImGui.MenuItem("Quit", "Alt+F4")) Console.WriteLine("Close Window");

            ImGui.EndMenu();
        }

        ImGui.Dummy(new System.Numerics.Vector2(ImGui.GetWindowWidth() -1000, 0));
        // ImGui.TextDisabled("Objects: " + (Objects.Count).ToString());
        ImGui.TextDisabled(" | ");
        ImGui.TextDisabled(GL.GetString(StringName.Renderer));
        ImGui.TextDisabled(" | ");
        ImGui.TextDisabled("FPS: " + ImGui.GetIO().Framerate.ToString("0"));
        ImGui.TextDisabled(" | ");
        ImGui.TextDisabled("MS: " + (1000 / ImGui.GetIO().Framerate).ToString("0.00"));

        ImGui.EndMainMenuBar();
    }


        





    public static void InputWindow()
    {
        ImGui.Begin("Demo");
        ImGui.SliderFloat("Vert1X", ref Vertices[0], -1.0f, 1.0f);
        ImGui.SliderFloat("Vert1Y", ref Vertices[1], -1.0f, 1.0f);
        ImGui.Separator();
        ImGui.SliderFloat("Vert2X", ref Vertices[3], -1.0f, 1.0f);
        ImGui.SliderFloat("Vert2Y", ref Vertices[4], -1.0f, 1.0f);
        ImGui.Separator();
        ImGui.SliderFloat("Vert3X", ref Vertices[6], -1.0f, 1.0f);
        ImGui.SliderFloat("Vert3Y", ref Vertices[7], -1.0f, 1.0f);
        ImGui.Separator();

        ImGui.SliderFloat("Speed", ref Mod, 0.0f, 1.0f);
        ImGui.SliderFloat("Bias", ref Bias, -0.5f, 0.5f);
        ImGui.Separator();

        ImGui.SliderFloat("Circ X", ref CircOrigin.X, -1.0f, 1.0f);
        ImGui.SliderFloat("Circ Y", ref CircOrigin.Y, -1.0f, 1.0f);
        ImGui.SliderFloat("Circ Z", ref CircOrigin.Z, -1.0f, 1.0f);
        ImGui.Separator();
        ImGui.SliderFloat("Size", ref CircSize, 0.0f, 0.1f);

        ImGui.Separator();
        ImGui.SliderFloat("Mult", ref mult, 0.0f, 100.0f);
        ImGui.SliderFloat("mMult", ref mMult, 0.0f, 1.0f);

        ImGui.Separator();
        ImGui.ColorPicker4("color", ref ColorPicked);
        ImGui.End();
    }

    public static void ValueWindow()
    {
        ImGui.Begin("Values");

        ImGui.Separator();
        ImGui.Text("R: " + ColorPicked.X.ToString());
        ImGui.Text("G: " + ColorPicked.Y.ToString());
        ImGui.Text("B: " + ColorPicked.Z.ToString());
        ImGui.Text("A: " + ColorPicked.W.ToString());
        ImGui.Separator();

        ImGui.Text("ObjectsRendered: " + ObjectsRendered.ToString());
        ImGui.Separator();

        ImGui.Text("Render Processing (ms): " + RenderProcessing.ToString());
        ImGui.Separator();

        ImGui.End();
    }

    public override void RenderGUI(ref float _cameraWidth, ref float _cameraHeight, ref int _framebufferTexture)
    {
        RenderWindows();
        ImGui.Begin("OpenGL");

        _cameraWidth = ImGui.GetWindowWidth();
        _cameraHeight = ImGui.GetWindowHeight() - ImGui.GetIO().FontGlobalScale * 71;

        ImGui.Image((IntPtr)_framebufferTexture,
            new System.Numerics.Vector2(_cameraWidth, _cameraHeight),
            new System.Numerics.Vector2(0, 0.95f),
            new System.Numerics.Vector2(1, 0),
            new System.Numerics.Vector4(1.0f),
            new System.Numerics.Vector4(1, 1, 1, 0.2f));
        ImGui.End();
    }
}