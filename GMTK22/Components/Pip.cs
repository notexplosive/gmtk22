using ExTween;
using ExTween.MonoGame;
using Microsoft.Xna.Framework;

namespace GMTK22.Components
{
    public class Pip
    {
        public static readonly Vector2 TopRight = new Vector2(1, -1);
        public static readonly Vector2 BottomLeft = new Vector2(-1, 1);
        public static readonly Vector2 BottomRight = new Vector2(1, 1);
        public static readonly Vector2 TopLeft = new Vector2(-1, -1);
        public static readonly Vector2 Center = new Vector2(0, 0);
        public static readonly Vector2 Left = new Vector2(-1, 0);
        public static readonly Vector2 Right = new Vector2(1, 0);
        public TweenableVector2 LocalPosition { get; } = new TweenableVector2();
        public TweenableInt IsVisible { get; } = new TweenableInt(1);
        public TweenableFloat RadiusPercent { get; } = new TweenableFloat(1);
    }
}
