using Godot;
using System;
using wizardgame.levels;

namespace wizardgame.utils
{
    public partial class Timer : Node
    {
        public double Time { get; set; }
        double TimerLength;
        public bool IsDone = false;
        public event Action<Timer> Finished;
        Level level;

        public Timer(double len, Level lev)
        {
            TimerLength = len;
            level = lev;
            level.CallDeferred("add_child", this); // add_child instead of AddChild
        }

        public override void _Process(double delta)
        {
            Time += delta;
            if (Time > TimerLength)
            {
                IsDone = true;
                Finished?.Invoke(this);
            }
        }

        public void Reset()
        {
            IsDone = false;
            Time = 0;
        }
    }
}
