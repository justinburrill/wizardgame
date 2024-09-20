using Godot;
using System;

namespace wizardgame.scripts
{
    public partial class HealthBar : Bar
    {

        public override void _Ready()
        {
            base._Ready();
            colour = Color.FromHtml("fe3149");
        }

        public override void _Process(double delta)
        {
            base._Process(delta);
        }
    }
}
