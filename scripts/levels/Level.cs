using Godot;
using System;
using wizardgame.characters;
using wizardgame.utils;

namespace wizardgame.levels
{
    public abstract partial class Level : Node2D
    {
        protected WaveManager waveManager;
        protected Player player;
        StaticBody2D borders;

        public override void _Ready()
        {
            borders = GetNode<StaticBody2D>("Borders");
            if (borders is null)
            {
                throw new NullReferenceException("Borders not found");
            }
            player = GetNode<Player>("./Player");
            if (player is null)
            {
                throw new NullReferenceException("Level couldn't find ref to player node");
            }
            waveManager = new(this, player);
        }

        public override void _Process(double delta)
        {
        }

        public Rect GetPlayAreaPosRect()
        {
            if (borders is null)
            {
                throw new NullReferenceException("Borders not set to reference");
            }
            if (borders.GetChildCount() != 4)
            {
                throw new NotImplementedException("unexpected number of borders");
            }
            Vector2 pos = new();
            float bottomY = 0;
            float rightX = 0;

            for (int i = 0; i < 4; i++)
            {
                var b = borders.GetChild<CollisionShape2D>(i);
                var deg = b.RotationDegrees.RoundToInt();
                switch (deg)
                {
                    // assuming 0 is up
                    case 0:
                        bottomY = b.Position.Y;
                        break;
                    case 90:
                        pos.X = b.Position.X;
                        break;
                    case 180:
                        pos.Y = b.Position.Y;
                        break;
                    case 270:
                        rightX = b.Position.X;
                        break;
                    default:
                        throw new NotImplementedException($"unexpected rotation {deg}");
                }
            }
            var w = rightX - pos.X;
            var h = bottomY - pos.Y;
            return new Rect(pos, new Vector2(w, h));
        }
    }



}
