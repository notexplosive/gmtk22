using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GMTK22.Components
{
    public class HoverFeedback : BaseComponent
    {
        private readonly BoundingRect boundingRect;
        private readonly Hoverable hoverable;

        public HoverFeedback(Actor actor) : base(actor)
        {
            this.boundingRect = RequireComponent<BoundingRect>();
            this.hoverable = RequireComponent<Hoverable>();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.hoverable.IsHovered && BusyFlags == 0)
            {
                var rect = this.boundingRect.Rect;
                rect.Inflate(10, 10);
                spriteBatch.DrawRectangle(rect, Color.Cyan.WithMultipliedOpacity(0.5f), 5, transform.Depth - 15);
            }
        }

        public int BusyFlags { get; set; }
    }
}
