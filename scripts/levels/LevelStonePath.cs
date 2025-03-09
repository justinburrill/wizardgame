using Godot;
using System;

namespace wizardgame.levels
{
    public partial class LevelStonePath : Level
    {

        public override void _Ready()
        {
            base._Ready();
            waveManager = new(this, this.player, Wave.ExampleWaves());
            waveManager.StartWaves();
        }

        public override void _Process(double delta)
        {
        }
    }
}
