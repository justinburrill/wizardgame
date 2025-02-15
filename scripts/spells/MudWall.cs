using Godot;
using System;

namespace wizardgame.spells
{
    public partial class MudWall : Spell
    {
        float lifetime = 5;
        bool dead = false;
        MudWall()
        {

            ManaCost = 60;
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            lifetime -= (float)delta;
            if (lifetime < 0)
            {
                dead = true;
                Play("die");
            }
        }


        public override void Cast(Player player, Vector2 dir, Level level)
        {
            float spawndist = 120;
            float yshift = 85;
            Position = player.Position + dir * spawndist - new Vector2(0, yshift);
            level.AddChild(this);
            Play("wall");
        }
        public void _on_animation_finished()
        {
            if (dead)
            {
                QueueFree();
            }
        }
    }
}
