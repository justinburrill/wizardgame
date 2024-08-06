using Godot;
using System;
using System.Security.AccessControl;
using wizardgame.scripts;
public partial class Player : Character
{
    AnimatedSprite2D sprite;
    HealthBar healthBar;
    private float _Health;

    public override float Health
    {
        get { return _Health; }
        set
        {
            _Health = value;
            healthBar.MaxHealthValue = MaxHealth;
            healthBar.Health = value;
        }
    }

    public override void _Ready()
    {
        sprite = GetChild<AnimatedSprite2D>(1);
        healthBar = GetChild<HealthBar>(2);
        Health = 150;
        Accel = 400;
        Decel = 900;
        MaxSpeed = 900;
        MaxHealth = 200;
    }

    public override void _Process(double delta)
    {
        var move = new Vector2();
        var moveKeyPressed = false;
        if (Input.IsKeyPressed(Key.W))
        {
            move += new Vector2(0, -1);
        }
        if (Input.IsKeyPressed(Key.S))
        {
            move += new Vector2(0, 1);
        }
        if (Input.IsKeyPressed(Key.A))
        {
            move += new Vector2(-1, 0);
            sprite.Play();
        }
        if (Input.IsKeyPressed(Key.D))
        {
            move += new Vector2(1, 0);
        }
        moveKeyPressed = !move.Equals(new Vector2());
        if (!moveKeyPressed)
        {
            sprite.Stop();
        }

        Velocity += move.Normalized() * Accel;
        Velocity = Velocity.LimitLength(MaxSpeed) * (float)delta;
        MoveAndCollide(Maths.Yscale(Velocity, yscale));

    }
}
