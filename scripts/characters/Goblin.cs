using Godot;
using Godot.Collections;
using System;
using System.Linq;
using wizardgame.scripts.utils;

namespace wizardgame.scripts
{
    public partial class Goblin : Character
    {
        Area2D SwingArea;
        AnimatedSprite2D sprite;
        utils.Timer SwingTimer;
        const float SwingDelay = 5;
        float SwingDamage = 10;

        public override void _Ready()
        {
            base._Ready();

            sprite = GetChild<AnimatedSprite2D>(0);
            SwingArea = GetChild<Area2D>(1);
            healthBar = GetChild<HealthBar>(2);


            //MaxHealth = 100;
            //Accel = 300;
            //Decel = 400;
            //MaxSpeed = 200;
            //Mass = 60;
            InitProperties(100, 300, 400, 230, 60);
            Health = MaxHealth;

            SwingTimer = new utils.Timer(SwingDelay, level);
        }
        public override void _Process(double delta)
        {
            base._Process(delta);
            // move
            {
                Vector2 move;
                var towardsPlayer = Maths.VectorTowards(player.Position, Position);
                move = towardsPlayer;

                var goblins = GetNearbyGoblins();
                foreach (var goblin in goblins)
                {
                    move -= Maths.VectorTowards(goblin.Position, Position);
                }
                Move(move, (float)delta);
            }

            // swing
            {
                if (SwingTimer.Done && PlayerInRange())
                {
                    sprite.Play("swing_face");
                    SwingTimer.Reset();
                }
            }
        }

        public Array<Node2D> GetNearbyGoblins()
        {

            var goblins = SwingArea.GetOverlappingBodies();
            for (int i = 0; i < goblins.Count; i++)
            {
                var x = goblins[i];
                if (x as Goblin == null)
                {
                    goblins.Remove(x);
                }
            }
            return goblins;
        }

        public void _on_animated_sprite_2d_animation_finished()
        {
            if (PlayerInRange())
            {
                player.Damage(SwingDamage);
            }
            SwingTimer.Reset();

            sprite.Play("default");
        }

        public bool PlayerInRange()
        {
            return SwingArea.OverlapsBody(player);
        }

    }
}
