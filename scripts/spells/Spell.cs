using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wizardgame.scripts.utils;

namespace wizardgame.scripts
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
