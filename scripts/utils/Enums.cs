
using static wizardgame.utils.Maths;

namespace wizardgame.utils;

public enum AttackButton
{
    LT,
    RT,
    LB,
    RB,
}
public enum SpellType
{
    FireBall,
    MudWall,
    Freeze,
}
public enum Element
{
    Air,
    Fire,
    Earth,
    Water,
}

public enum Direction4
{
    Up,
    Down,
    Left,
    Right,
}

public enum Direction8
{
    Up,
    UpRight,
    Right,
    DownRight,
    Down,
    DownLeft,
    Left,
    UpLeft,
}
public enum TextAction
{
    Append,
    Remove,
    Set,
    Replace,
}

public static class Direction4Extensions
{
    public static Direction4 Random()
    {
        return (Direction4)RandomInARange(0, 4);
    }
}

public enum StatusEffect
{
    Frozen,
}
