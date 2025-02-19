using Godot;
using System;

namespace wizardgame.hud
{
    public partial class ManaBar : Bar
    {

        public override void _Ready()
        {
            base._Ready();
            colour = Color.FromHtml("4898cb");
            DmgBar.Visible = false;
        }

        public override void _Process(double delta)
        {
            base._Process(delta);
        }
    }
}
