using Godot;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wizardgame.scripts
{
    public static class Maths
    {
        public static Godot.Vector2 Yscale(Vector2 vec, float yscale)
        {
            return new Vector2(vec.X, vec.Y * yscale);
        }
        public static float RandomInARange(float lower, float upper)
        {
            if (upper < lower)
            {
                throw new ArgumentException("upper and lower bounds mismatched");
            }
            Random random = new();
            float diff = upper - lower;


            var result = (float)random.NextDouble() * diff + lower;
            //GD.Print($"lower: {lower} upper: {upper} result: {result}");
            return result;
        }

        public static Godot.Vector2 RandomDirection()
        {
            float angle = RandomInARange(0, (float)(2 * Math.PI));
            var result = Godot.Vector2.FromAngle(angle);
            //Console.WriteLine(result);
            return result;
        }

        // this totally suck
        public static Godot.Vector2 InfluenceVector(Godot.Vector2 original, Godot.Vector2 desired, float amount)
        {
            if (!(0 < amount && amount < 1)) { throw new ArgumentOutOfRangeException(); }
            amount = amount * 2;
            Godot.Vector2 output = new();
            output.X = original.X + desired.X * amount;
            output.Y = original.Y + desired.Y * amount;
            return output.Normalized();
        }

        public static double AngleBetween(Godot.Vector2 one, Godot.Vector2 two)
        {
            var angleOne = Math.Atan(one.Y / one.X);
            var angleTwo = Math.Atan(two.Y / two.X);
            return angleTwo - angleOne;
        }

        public static Godot.Vector2 VectorTowards(Godot.Vector2 destination, Godot.Vector2 myPos)
        {
            return VectorBetween(destination, myPos).Normalized();
        }

        public static Godot.Vector2 VectorBetween(Godot.Vector2 destination, Godot.Vector2 myPos)
        {
            var x = destination.X - myPos.X;
            var y = destination.Y - myPos.Y;

            return new Godot.Vector2(x, y);
        }


        public static Godot.Vector2 Vectorize(float num) { return new Godot.Vector2(num, num); }
        public static Godot.Vector2 Vectorize(double num) { return new Godot.Vector2((float)num, (float)num); }

    }
}
