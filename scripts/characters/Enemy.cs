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
            //var camref = GetNode<Camera2D>("Camera");
            //var camArea = new Rect(camref.Position, camref.GetViewportRect().Size);
            var worldArea = level.GetPlayAreaPosRect();
            float dist = 50;
            Vector2 pos = Direction4Extensions.Random() switch
            {
                Direction4.Up => new Vector2(RandomInARange(worldArea.X, worldArea.RightX), worldArea.Y + dist),
                Direction4.Down => new Vector2(RandomInARange(worldArea.X, worldArea.RightX), worldArea.BottomY - dist),
                Direction4.Left => new Vector2(worldArea.X - dist, RandomInARange(worldArea.Y, worldArea.BottomY)),
                Direction4.Right => new Vector2(worldArea.RightX + dist, RandomInARange(worldArea.Y, worldArea.BottomY)),
                _ => throw new NotImplementedException("unexpected direction"),
            };

            Spawn(pos);
        }
    }
}
