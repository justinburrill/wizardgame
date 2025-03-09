using Godot;

using wizardgame.hud;
using wizardgame.levels;
using wizardgame.utils;

namespace wizardgame.characters;

public partial class Goblin : Enemy
{
    private Area2D SwingArea;
    private utils.Timer SwingTimer;
    private const float SwingDelay = 5;
    private float SwingDamage = 10;

    public override void _Ready()
    {
        base._Ready();

        //GD.Print($"Inside goblin ready: GetChildCount(): {GetChildCount()}");

        Sprite = GetChild<AnimatedSprite2D>(0);
        SwingArea = GetChild<Area2D>(1);
        healthBar = GetChild<HealthBar>(2);

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
            var scale = 0.6f;
            foreach (var goblin in enemies)
            {
                // avoid other goblins so they don't pile up
                move -= scale * Maths.VectorTowards(goblin.Position, Position);
            }
            Move(move, (float)delta);
        }

        // swing
        {
            if (SwingTimer.IsDone && PlayerInRange())
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
