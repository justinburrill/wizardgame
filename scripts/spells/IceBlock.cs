using Godot;
using System;

namespace wizardgame.scripts.spells
{
    public partial class IceBlock : AnimatedSprite2D
    {
        public float lifetime = 5;
        public Character Target;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            Position = Target.Position + new Vector2(0, -50);
            Target.TreeExiting += Target_TreeExiting;
        }

        private void Target_TreeExiting()
        {
            QueueFree();
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            lifetime -= (float)delta;
            if (lifetime < 0)
            {
                QueueFree();
                //Target.Frozen = false;
                Target.statusEffects.Remove(utils.StatusEffect.Frozen);
            }
        }
    }
}
