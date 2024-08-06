using Godot;
using System;

namespace wizardgame.scripts
{
    public partial class Goblin : Character
    {
        Player player;
        Area2D SwingArea;
        float SwingTimer = 0;
        float DefaultSwingTimer = 1.5f;
        float SwingDamage = 10;
        public override float Health { get; set; }

        public override void _Ready()
        {
            Accel = 10;
            MaxSpeed = 200;

            player = GetNode<Player>("/root/Level/Player");
            SwingArea = GetNode<Area2D>("/root/Level/Goblin/SwingArea");



        }
        public override void _Process(double delta)
        {
            // move
            {
                var towardsPlayer = Maths.VectorTowards(player.Position, Position);
                Velocity = (Velocity.Length() + Accel * (float)delta) * towardsPlayer;
                Position += Velocity.LimitLength(MaxSpeed) * (float)delta;


            }

            // swing
            {
                SwingTimer -= (float)delta;

                if (SwingArea.OverlapsBody(player))
                {
                    if (SwingTimer < 0)
                    {
                        player.Damage(SwingDamage);
                        GD.Print($"player health now at {player.Health}");
                        SwingTimer = DefaultSwingTimer;
                    }
                }

            }
        }

    }
}
