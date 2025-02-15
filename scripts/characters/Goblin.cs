using Godot;
using wizardgame.hud;
using wizardgame.levels;
using wizardgame.utils;

namespace wizardgame.characters
{
    public partial class Goblin : Enemy
    {
        Area2D SwingArea;
        utils.Timer SwingTimer;
        const float SwingDelay = 5;
        float SwingDamage = 10;

        public override void _Ready()
        {
            base._Ready();

            Sprite = GetChild<AnimatedSprite2D>(0);
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

                var enemies = GetNearbyEnemies(SwingArea);
                foreach (var goblin in enemies)
                {
                    move -= Maths.VectorTowards(goblin.Position, Position);
                }
                Move(move, (float)delta);
            }

            // swing
            {
                if (SwingTimer.Done && PlayerInRange())
                {
                    Sprite.Play("swing_face");
                    SwingTimer.Reset();
                }
            }
        }


        public void _on_animated_sprite_2d_animation_finished()
        {
            if (PlayerInRange())
            {
                player.Damage(SwingDamage);
            }
            SwingTimer.Reset();

            Sprite.Play("default");
        }

        public bool PlayerInRange()
        {
            return SwingArea.OverlapsBody(player);
        }

    }
}
