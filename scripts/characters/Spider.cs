using Godot;
using System;
namespace wizardgame.characters
{
    public partial class Spider : Character
    {
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            InitProperties(60, 1000, 1000, 1500, 40);
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }
    }
}
