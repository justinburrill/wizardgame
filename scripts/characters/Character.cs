using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wizardgame.scripts
{
    public abstract partial class Character : CharacterBody2D
    {
        public float yscale = 0.8f;
        public bool Frozen = false;
        private float _health;
        public float Health
        {
            get { return _health; }
            set
            {
                _health = value;
                healthBar.MaxValue = MaxHealth;
                healthBar.Amount = value;
            }
        }
        public float MaxHealth;
        public float MaxSpeed;
        public float Accel;
        public float Mass;
        public const float FrictionCoefficient = 80;
        public float Decel;
        public HealthBar healthBar;

        public void Damage(float amount)
        {
            Health -= amount;
            if (Health <= 0)
            {
                Kill();
            }
        }

        public void ApplyFrictionToVelocity(double delta)
        {
            var frictionDir = -(Velocity.Normalized());

            float frictionDecel = FrictionCoefficient * Mass;


            //GD.Print($"Dir: {Velocity.Normalized()} FrictionDir: {frictionDir} Velocity: {Velocity}");
            Velocity += (frictionDir * (float)delta * frictionDecel).LimitLength(Velocity.Length());
        }

        public void Kill()
        {
            QueueFree();
        }
    }
}
