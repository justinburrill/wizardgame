using Godot;
using Godot.Collections;

namespace wizardgame.characters
{
    public abstract partial class Enemy : Character
    {
        public AnimatedSprite2D Sprite;
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

        public void Spawn()
        {
            level.AddChild(this);
        }
    }
}
