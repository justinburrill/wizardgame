using Godot;
using wizardgame.characters;
using wizardgame.levels;

namespace wizardgame.spells
{
    public abstract partial class Beam : Spell
    {
        public Vector2 Direction;
        public float HBLength;
        public float HBStartWidth;
        public float HBEndWidth;
        public double MaxTime;
        public double TimeLeft;

        public override void _Process(double delta)
        {
            base._Process(delta);

        }
        public abstract void OnCharacterImpact(Character o, Vector2 normal);
        public override void Cast(Player player, Vector2 dir, Level level)
        {
            // spawn beam

            throw new System.NotImplementedException();
        }
    }
}
