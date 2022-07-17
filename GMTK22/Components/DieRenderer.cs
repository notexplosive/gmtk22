using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GMTK22.Components
{
    public class DieRenderer : BaseComponent
    {
        private readonly Color bodyColor;
        private readonly Color pipColor;
        private readonly DieComponent die;
        private readonly BoundingRect boundingRect;

        public DieRenderer(Actor actor, Color bodyColor, Color pipColor) : base(actor)
        {
            this.bodyColor = bodyColor;
            this.pipColor = pipColor;
            this.boundingRect = RequireComponent<BoundingRect>();
            this.die = RequireComponent<DieComponent>();
            
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(this.boundingRect.Rect, this.bodyColor, transform.Depth);

            foreach (var pip in this.die.Pips)
            {
                var radius = this.boundingRect.Width / 10;
                var circle = new CircleF(transform.Position + pip.LocalPosition.Value * this.boundingRect.Width / 4f, radius);
                spriteBatch.DrawCircle(circle, 10, this.pipColor, radius, transform.Depth - 10);
            }

            spriteBatch.DrawRectangle(this.boundingRect.Rect, this.pipColor, 3f, transform.Depth - 1);
        }
    }
}
