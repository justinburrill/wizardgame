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
        public float yscale = 0.6f;
        public abstract float Health { get; set; }
        public float MaxHealth;
        public float MaxSpeed;
        public float Accel;
        public float Decel;

        public void Damage(float amount)
        {
            Health -= amount;
            if (Health < 0)
            {
                Kill();
            }
        }

        public void Kill()
        {
            GD.Print("something died");
            //QueueFree();
        }
    }
}
