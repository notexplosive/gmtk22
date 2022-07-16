using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GMTK22
{
    public class Direction
    {
        public static readonly Direction Invalid = new Direction();
        public static readonly Direction Up = new Direction(new Point(0, -1), "Up");
        public static readonly Direction Down = new Direction(new Point(0, 1), "Down");
        public static readonly Direction Left = new Direction(new Point(-1, 0), "Left");
        public static readonly Direction Right = new Direction(new Point(1, 0), "Right");
        private readonly string name;

        private Direction()
        {
            IsValid = false;
        }

        private Direction(Point direction, string name)
        {
            IsValid = true;
            Position = direction;
            this.name = name;
        }

        public Point Position { get; }

        public bool IsValid { get; }

        public override string ToString()
        {
            if (IsValid)
            {
                return this.name;
            }
            else
            {
                return "Invalid";
            }
        }

        public static Direction FromKey(Keys key)
        {
            switch (key)
            {
                case Keys.Left:
                    return Direction.Left;
                case Keys.Right:
                    return Direction.Right;
                case Keys.Down:
                    return Direction.Down;
                case Keys.Up:
                    return Direction.Up;
                default:
                    return Direction.Invalid;
            }
        }
    }
}
