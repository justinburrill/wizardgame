using Godot;
using System;
using wizardgame.characters;
using wizardgame.levels;
using wizardgame.utils;
namespace wizardgame.scripts
{
    public partial class Camera : Godot.Camera2D
    {
        Vector2 Velocity = new();
        float MaxDist = 100;
        Level level;
        Player player;
        Sprite2D bg;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            level = GetTree().Root.GetChild<Level>(0);
            player = level.GetNode<Player>("./Player");
            bg = level.GetNode<Sprite2D>("Background");
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            // Move background and camera to player (jank alert!!)
            {

                Velocity = Maths.VectorBetweenPoints(player.Position, Position);
                var towardsPlayer = Maths.VectorTowards(player.Position, Position);
                var l = Velocity.Length();
                if (l > MaxDist)
                {

                    Position += (Velocity - towardsPlayer * MaxDist);
                }


                Position += Velocity * (float)delta * 5;


                // Moves bg along with camera
                bg.Position = Position;
            }



        }

    }
}
