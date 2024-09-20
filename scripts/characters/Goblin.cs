using Godot;
using System;
using System.Linq;

namespace wizardgame.scripts
{
    public partial class Goblin : Character
    {
        Player player;
        Area2D SwingArea;
        AnimatedSprite2D sprite;
        float SwingTimer = 0;
        float DefaultSwingTimer = 1.5f;
        float SwingDamage = 10;

        public override void _Ready()
        {

            player = GetNode<Player>("/root/Level/Player");
            sprite = GetChild<AnimatedSprite2D>(0);
            SwingArea = GetChild<Area2D>(1);
            healthBar = GetChild<HealthBar>(2);


            MaxHealth = 100;
            Health = MaxHealth;
            Accel = 100;
            MaxSpeed = 200;


        }
        public override void _Process(double delta)
        {
            // move
            {
                Vector2 move;
                var towardsPlayer = Maths.VectorTowards(player.Position, Position);
                move = towardsPlayer;

                var goblins = SwingArea.GetOverlappingBodies();
                foreach (Node2D x in goblins)
                {
                    if (x as Goblin == null)
                    {
                        goblins.Remove(x);
                    }

                }
                foreach (var goblin in goblins)
                {
                    move += Maths.VectorTowards(goblin.Position, Position);
                }


                Velocity = (Velocity.Length() + Accel * (float)delta) * move.Normalized();
                if (Frozen) { Velocity = new Vector2(0, 0); }
                MoveAndCollide(Maths.Yscale(Velocity.LimitLength(MaxSpeed) * (float)delta, yscale));

                if (move.Equals(0))
                {
                    ApplyFrictionToVelocity(delta);

                }


            }

            // swing
            {
                SwingTimer -= (float)delta;

                if (SwingTimer < 0 && PlayerInRange())
                {

                    sprite.Play("swing_face");
                }

            }
        }

        public void _on_animated_sprite_2d_animation_finished()
        {
            if (PlayerInRange())
            {
                player.Damage(SwingDamage);
            }
            SwingTimer = DefaultSwingTimer;

            sprite.Play("default");
        }

        public bool PlayerInRange()
        {
            return SwingArea.OverlapsBody(player);
        }

    }
}
