using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GMTK22.Components
{
    public class DieRenderer : BaseComponent
    {
        private readonly DieComponent die;
        private readonly BoundingRect boundingRect;

        public DieRenderer(Actor actor) : base(actor)
        {
            this.boundingRect = RequireComponent<BoundingRect>();
            this.die = RequireComponent<DieComponent>();
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(this.boundingRect.Rect, Color.White, transform.Depth);

            foreach (var pip in this.die.Pips)
            {
                var radius = this.boundingRect.Width / 15;
                var circle = new CircleF(transform.Position + pip.LocalPosition.Value * this.boundingRect.Width / 2f * 0.65f, radius);
                spriteBatch.DrawCircle(circle, 10, Color.Black, radius);
            }

            spriteBatch.DrawRectangle(this.boundingRect.Rect, Color.Black, 3f, transform.Depth - 1);
        }
    }
}
