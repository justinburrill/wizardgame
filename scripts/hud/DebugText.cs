using Godot;
using wizardgame.scripts;
using wizardgame.utils;

namespace wizardgame.hud
{
    public delegate void DebugTextUpdateHandler(object sender, DebugTextUpdateArgs args);
    public class DebugTextUpdateArgs : System.EventArgs
    {
        public TextAction Action;
        public string Text;
        public string? OldText = null;
    }
    public partial class DebugText : Label
    {
        Camera camera;
        public DebugText(Camera camera)
        {
            this.camera = camera;
        }

        public override void _Ready()
        {
            base._Ready();
        }

        public override void _Process(double delta)
        {
            base._Process(delta);
            var offset = 5;
            Position = camera.Position + new Vector2(offset, offset);
        }
    }
}
