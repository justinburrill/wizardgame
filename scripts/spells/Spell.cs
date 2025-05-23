﻿using Godot;
using System;
using wizardgame.characters;
using wizardgame.levels;
using wizardgame.utils;

namespace wizardgame.spells
{
    public abstract partial class Spell : AnimatedSprite2D
    {
        public float ManaCost;
        public Element element;
        public SpellType type;

        public abstract void Cast(Player player, Vector2 dir, Level level);

        //public static Spell GetSpellFromSpellType(SpellType st)
        //{
        //    Type t = Type.GetType(st.ToString());
        //}

        public static PackedScene SpellSceneFromSpellType(SpellType t)
        {
            var x = t switch
            {
                SpellType.FireBall => ResourceLoader.Load<PackedScene>("res://scenes/FireBall.tscn"),
                SpellType.MudWall => ResourceLoader.Load<PackedScene>("res://scenes/MudWall.tscn"),
                SpellType.Freeze => ResourceLoader.Load<PackedScene>("res://scenes/Freeze.tscn"),
                _ => throw new NotImplementedException()
            };


            return x;
        }
    }
}
