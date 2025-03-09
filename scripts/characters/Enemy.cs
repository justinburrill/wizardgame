using System;

using Godot;
using Godot.Collections;

using wizardgame.levels;
using wizardgame.utils;

using static wizardgame.utils.Maths;

namespace wizardgame.characters;

public abstract partial class Enemy : Character
{
    public Player player;

    public override void _Ready()
    {
        base._Ready();
        player = level.GetNode<Player>("./Player");
    }

    public Array<Enemy> GetNearbyEnemies(Area2D area)
    {
        var bodies = area.GetOverlappingBodies();
        var enemies = new Array<Enemy>();
        for (int i = 0; i < bodies.Count; i++)
        {
            var x = bodies[i];
            if ((x as Enemy) != null)
            {
                enemies.Add(x as Enemy);
            }
        }
        return enemies;
    }

    //public void Spawn(Vector2 pos, Level level)
    //{
    //    if (level is null)
    //    {
    //        throw new NullReferenceException("reference to level not set in Spawn");
    //    }
    //    level.AddChild(this);
    //    Position = pos;
    //}
    public virtual void Spawn(Vector2 pos, Level level)
    {
        // TODO: don't have this be loaded for every enemy spawn
        var scene = GD.Load<PackedScene>("res://scenes/Goblin.tscn");
        Enemy instance = scene.Instantiate<Enemy>();
        instance.Position = pos;
        level.AddChild(instance);
    }

    public virtual void SpawnFromOffScreen(Level level)
    {
        //var camref = GetNode<Camera2D>("Camera");
        //var camArea = new Rect(camref.Position, camref.GetViewportRect().Size);
        if (level is null)
        {
            throw new NullReferenceException("reference to level not set in spawn from offscreen");
        }
        var worldArea = level.GetPlayAreaPosRect();
        //GD.Print(worldArea);
        float dist = 50;
        Vector2 pos = Direction4Extensions.Random() switch
        {
            Direction4.Up => new Vector2(RandomInARange(worldArea.X, worldArea.RightX), worldArea.Y + dist),
            Direction4.Down => new Vector2(RandomInARange(worldArea.X, worldArea.RightX), worldArea.BottomY - dist),
            Direction4.Left => new Vector2(worldArea.X - dist, RandomInARange(worldArea.Y, worldArea.BottomY)),
            Direction4.Right => new Vector2(worldArea.RightX + dist, RandomInARange(worldArea.Y, worldArea.BottomY)),
            _ => throw new NotImplementedException("unexpected direction"),
        };

        GD.Print($"Spawning enemy from offscreen at {pos}");

        Spawn(pos, level);
    }
}
