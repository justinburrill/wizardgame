using System;

using Godot;

namespace wizardgame.utils;

public static class Maths
{
    public static Vector2 Yscale(Vector2 vec, float yscale)
    {
        return new Vector2(vec.X, vec.Y * yscale);
    }
    public static float RandomInARange(float upper)
    {
        return RandomInARange(0, upper);
    }
    public static float RandomInARange(float lower, float upper)
    {
        if (upper < lower)
        {
            throw new ArgumentException("upper and lower bounds mismatched");
        }
        Random random = new();
        float diff = upper - lower;

        var result = ((float)random.NextDouble() * diff) + lower;
        //GD.Print($"lower: {lower} upper: {upper} result: {result}");
        return result;
    }
    public static int RoundToInt(this float num)
    {
        return (int)Math.Round(num);
    }

    public static Vector2 RandomDirection()
    {
        float angle = RandomInARange(0, (float)(2 * Math.PI));
        var result = Vector2.FromAngle(angle);
        //Console.WriteLine(result);
        return result;
    }

    // this totally suck
    public static Vector2 InfluenceVector(Vector2 original, Vector2 desired, float amount)
    {
        if (amount is not (> 0 and < 1)) { throw new ArgumentOutOfRangeException(); }
        amount *= 2;
        Vector2 output = new()
        {
            X = original.X + (desired.X * amount),
            Y = original.Y + (desired.Y * amount)
        };
        return output.Normalized();
    }

    public static float AngleBetween(Vector2 one, Vector2 two)
    {
        var angleOne = Math.Atan(one.Y / one.X);
        var angleTwo = Math.Atan(two.Y / two.X);
        return (float)(angleTwo - angleOne);
    }

    public static float AngleFromUp(Vector2 v)
    {
        return AngleBetween(Vector2.Up, v);
    }

    public static Vector2 VectorTowards(Vector2 destination, Vector2 myPos)
    {
        return VectorBetweenPoints(destination, myPos).Normalized();
    }

    /// <summary>
    /// VectorTowards() but with randomness
    /// </summary>
    /// <param name="destination">Location the vector is going to</param>
    /// <param name="mypos">Location the vector is coming from</param>
    /// <param name="randomness">Float from zero to one (will be clamped, values >1 are the same as 1) representing the amount of randomness</param>
    /// <returns>
    /// If randomness is 0, then the vector between the two points will be given.
    /// If randomness is 1, then it will return a vector anywhere between 90deg to the left and 90deg to the right of the destination.
    ///</returns>
    public static Vector2 VectorTowardsWithRandomness(Vector2 destination, Vector2 mypos, float randomness)
    {
        randomness = Math.Clamp(Math.Abs(randomness), 0, 1);
        var rand = new Random();

        var towards = VectorTowards(destination, mypos);
        var spread = Math.PI * randomness;
        var angle = throw new NotImplementedException();

        return towards.Rotated();
    }

    public static Vector2 VectorBetweenPoints(Vector2 destination, Vector2 myPos)
    {
        var x = destination.X - myPos.X;
        var y = destination.Y - myPos.Y;

        return new Vector2(x, y);
    }

    public static Direction4 GetVectorDirection4(Vector2 vec)
    {
        var pi = Math.PI;
        float angle = vec.Angle(); // from -pi to pi
        //GD.Print($"{angle} {angle * 180 / pi}");
        return angle switch
        {
            var x when x < pi / 4 && x > -pi / 4 => Direction4.Right,
            var x when x < -pi / 4 && x > 3 * -pi / 4 => Direction4.Up,
            var x when x > 3 * pi / 4 || x < -3 * pi / 4 => Direction4.Left,
            var x when x > pi / 4 && x < 3 * pi / 4 => Direction4.Down,
            _ => throw new Exception($"GetVectorDirection4 does not work - angle:{angle}"),
        };
    }

    public static Vector2 Vectorize(float num) { return new Vector2(num, num); }
    public static Vector2 Vectorize(double num) { return new Vector2((float)num, (float)num); }

    public static float RandomFrom1ToNeg1()
    {
        Random rand = new();
        return rand.NextSingle() > 0.5f ? rand.NextSingle() : -rand.NextSingle();
    }
}
