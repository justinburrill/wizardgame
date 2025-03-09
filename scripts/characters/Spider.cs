using System;

using Godot;
namespace wizardgame.characters;

public partial class Spider : Enemy
{

    private Vector2 currentMoveDir = new();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        InitProperties(60, 1000, 1000, 1500, 40);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        var playerpos = player.Position;
        if (currentMoveDir == new Vector2())
        {

        }
    }
}
