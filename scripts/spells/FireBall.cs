using Godot;
using System;
using wizardgame.scripts.utils;
namespace wizardgame.scripts
{

    public partial class FireBall : Projectile
    {
        //PackedScene fireballScene;
        float dmg = 25;
        CpuParticles2D Particles;
        FireBall()
        {
            ManaCost = 15;
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            Particles = GetChild<CpuParticles2D>(0);
            Hitbox = GetChild<Area2D>(1);
            //fireballScene = GD.Load<PackedScene>("res://scenes/FireBall.tscn");
            //GD.Print(fireballScene);
            Play("default");
            Velocity = 850;
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            base._Process(delta);
        }

        public override void OnCharacterImpact(Character overlapper, Vector2 normal)
        {
            Particles.Visible = false;

            Play("explode");
            Velocity = 0;
            overlapper.Damage(dmg);
            moving = false;


            var force = 10000;
            overlapper.ApplyForce(normal * force);
            Rotate(Maths.AngleBetween(Vector2.Up, normal));
        }
        public void _on_animation_finished()
        {
            QueueFree();
        }

    }
}
