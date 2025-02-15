using Godot;
using System;
using wizardgame.utils;

namespace wizardgame.levels
{
    public abstract partial class Level : Node2D
    {
        StaticBody2D borders;

        public override void _Ready()
        {
        }

        public override void _Process(double delta)
        {
        }

        public Rect GetPlayAreaDimensions()
        {
            Vector2 pos = new();
            float bottomY = 0;
            float rightX = 0;
            for (int i = 0; i < 4; i++)
            {
                var b = borders.GetChild<CollisionShape2D>(i);
                switch (b.RotationDegrees)
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
                        throw new NotImplementedException("unexpected location");
                }
            }
            var w = rightX - pos.X;
            var h = bottomY - pos.Y;
            return new Rect(pos, new Vector2(w, h));
        }
    }



}
