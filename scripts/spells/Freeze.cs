using Godot;
using System;
using System.Linq;

namespace wizardgame.scripts.spells
{
    public partial class Freeze : Projectile
    {
        float dmg = 5;
        Freeze()
        {
            ManaCost = 50;
        }
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            Hitbox = GetChild<Area2D>(0);
            Play("default");
            Velocity = 950;
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            base._Process(delta);
        }

        public override void OnCharacterImpact(Character c, Vector2 normal)
        {
            if (c.statusEffects.Contains(utils.StatusEffect.Frozen)) { return; }
            if (c is Player) { return; }
            //GD.Print("freeze em!!!");
            //c.Frozen = true; // mwhahaha!!
            c.statusEffects.Add(utils.StatusEffect.Frozen);
            var scene = ResourceLoader.Load<PackedScene>("res://scenes/IceBlock.tscn");
            var ib = (IceBlock)scene.Instantiate();
            ib.Target = c;
            GetTree().Root.AddChild(ib);


        }
    }
}
