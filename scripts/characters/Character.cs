using Godot;
using System;
using System.Collections.Generic;
using wizardgame.hud;
using wizardgame.levels;
using wizardgame.utils;

namespace wizardgame.characters
{
    public abstract partial class Character : CharacterBody2D
    {
        public Level level;
        public Player player;
        public float yscale = 0.8f;
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
        private float _maxHealth;
        public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
        public float MaxSpeed;
        public float Accel;
        public float Mass;
        public const float FrictionCoefficient = 15f;
        public float Decel;
        public HealthBar healthBar;
        public Vector2 shove;
        public List<utils.StatusEffect> statusEffects;

        public void InitProperties(float maxhealth, float accel, float decel, float maxspeed, float mass)
        {
            MaxHealth = maxhealth;
            Accel = accel;
            Decel = decel;
            MaxSpeed = maxspeed;
            Mass = mass;
            statusEffects = new List<StatusEffect>();
        }

        public bool IsAlive => Health > 0;

        public void Damage(float amount)
        {
            Health -= amount;
            if (Health <= 0)
            {
                Kill();
            }
        }

        public void ApplyForce(Vector2 force)
        {
            if (Mass == 0) { throw new DivideByZeroException(); }
            shove += force / Mass;
        }

        public void Move(Vector2 direction, float delta, float amount = 1)
        {
            if (statusEffects.Contains(StatusEffect.Frozen)) return;
            //GD.Print($"direction: {direction,10} Velocity: {Velocity,10} dot: {direction.Dot(Velocity),10}");
            //GD.Print($"direction: {direction.Normalized(),10} Velocity: {Velocity.Normalized(),10} dot: {direction.Normalized().Dot(Velocity.Normalized()),10}");


            if (direction.Length() > 0)
            {
                var dot = direction.Dot(Velocity.Normalized());
                bool decelerating = dot <= 0;
                float a = decelerating ? Accel : Decel;
                var newvel = Velocity + a * amount * delta * direction;

                Velocity = newvel.LimitLength(MaxSpeed);

                //Velocity += shove;
            }
            else
            {
                ApplyFrictionToVelocity(delta);
            }
            MoveAndCollide(Maths.Yscale(Velocity * (float)delta, yscale));
        }


        public override void _Process(double delta)
        {
        }

        public override void _Ready()
        {
            level = GetNode<Level>("/root/Level");
            player = GetNode<Player>("/root/Level/Player");
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
