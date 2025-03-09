using Godot;

namespace wizardgame.utils
{
    public class Rect
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;
        public float BottomY { get { return Y + Height; } }
        public float RightX { get { return X + Width; } }
        public Vector2 Position
        {
            get { return new Vector2(X, Y); }
            set { X = value.X; Y = value.Y; }
        }
        public Vector2 Size
        {
            get { return new Vector2(Width, Height); }
            set { Width = value.X; Height = value.Y; }
        }

        public Rect()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
        }
        public Rect(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Rect(Vector2 pos, Vector2 size)
        {
            X = pos.X;
            Y = pos.Y;
            Width = size.X;
            Height = size.Y;
        }



        public bool IsOverlapping(Rect other)
        {
            return X < other.X + other.Width &&
                   X + Width > other.X &&
                   Y < other.Y + other.Height &&
                   Y + Height > other.Y;
        }

        public bool Contains(Vector2 point, float tolerance = 0f)
        {
            bool inLeft = X - tolerance < point.X;
            bool inTop = Y - tolerance < point.Y;
            bool inRight = RightX + tolerance > point.X;
            bool inBottom = BottomY + tolerance > point.Y;
            return inLeft && inTop && inRight && inBottom;
        }

    }
}
