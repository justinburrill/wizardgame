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
        public AnimatedSprite2D Sprite;
        protected const float yscale = 0.8f;
        protected const float FrictionCoefficient = 15f;
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
        private float MaxSpeed;
        protected float Accel;
        private float _mass;
        public float Mass { get { return _mass; } set { _mass = value; } }
        private float Decel;
        protected HealthBar healthBar;
        private Vector2 shove;
        public List<utils.StatusEffect> statusEffects;
        public event Action<Character> Died;

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
            base._Ready(); // ?
            level = GetTree().Root.GetChild<Level>(0);
            if (level is null)
            {
                throw new NullReferenceException("Level not found");
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
            Died?.Invoke(this); // event for death
            QueueFree();
        }
    }
}
