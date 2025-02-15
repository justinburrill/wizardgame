using Godot;
using Godot.Collections;
using System;
using wizardgame.utils;
using static wizardgame.utils.Maths;

namespace wizardgame.characters
{
    public abstract partial class Enemy : Character
    {
        public Array<Enemy> GetNearbyEnemies(Area2D area)
        {
            var bodies = area.GetOverlappingBodies();
            var enemies = new Array<Enemy>();
            for (int i = 0; i < bodies.Count; i++)
            {
                var x = bodies[i];
                if (x as Enemy != null)
                {
                    enemies.Add(x as Enemy);
                }
            }
            return enemies;
        }

        public void Spawn(Vector2 pos)
        {
            level.AddChild(this);
            Position = pos;
        }

        public virtual void SpawnFromOffScreen()
        {
            var camref = GetNode<Camera2D>("Camera");
            var camArea = new Rect(camref.Position, camref.GetViewportRect().Size);
            float dist = 50;
            Direction4 randomDir = (Direction4)RandomInARange(0, 4);
            Vector2 pos = randomDir switch
            {
                Direction4.Up => new Vector2(RandomInARange(camArea.X, camArea.RightX), camArea.Y + dist),
                Direction4.Down => new Vector2(RandomInARange(camArea.X, camArea.RightX), camArea.BottomY - dist),
                Direction4.Left => new Vector2(camArea.X - dist, RandomInARange(camArea.Y, camArea.BottomY)),
                Direction4.Right => new Vector2(camArea.RightX + dist, RandomInARange(camArea.Y, camArea.BottomY)),
                _ => throw new NotImplementedException("unexpected direction"),
            };

            Spawn(pos);
        }
    }
}
