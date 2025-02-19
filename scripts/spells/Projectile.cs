using Godot;
using System.Collections.Generic;
using wizardgame.characters;
using wizardgame.levels;

namespace wizardgame.spells
{
    public abstract partial class Projectile : Spell
    {
        public float Velocity;
        public Vector2 Direction;
        public Area2D Hitbox;
        public bool moving = true;

        public override void _Process(double delta)
        {
            if (!moving) { return; }

            Move(delta);
            var overlappers = Hitbox.GetOverlappingBodies();
            for (int i = 0; i < overlappers.Count; i++)
            {
                var o = overlappers[i];
                var ignores = new List<string> { "Player", "Borders" };
                if (ignores.Contains(o.Name))
                {
                    overlappers.Remove(o);
                }
            }
            if (overlappers.Count > 0)
            {
                //var o = overlappers[0];
                //GD.Print($"impacted {overlappers[0].Name}");
                //foreach (Node2D o in overlappers)
                //{
                //    if (o is Character character)
                //    {
                //        OnCharacterImpact(character, Direction.Normalized());
                //    }
                //   }
                foreach (var o in overlappers)
                {
                    if (o is Character character)
                    {
                        OnCharacterImpact(character, Direction.Normalized());
                    }
                }

            }
        }

        public void Move(double delta)
        {
            Position += Direction * Velocity * (float)delta;
        }
        public abstract void OnCharacterImpact(Character o, Vector2 normal);

        public override void Cast(Player player, Vector2 dir, Level level)
        {
            Position = player.Position;
            Direction = dir;
            level.AddChild(this);
        }

    }

}
