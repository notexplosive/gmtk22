using System.Collections.Generic;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GMTK22.Components
{
    public class DieRenderer : BaseComponent
    {
        private readonly BoundingRect boundingRect;
        private readonly DieComponent dieComponent;

        public DieRenderer(Actor actor) : base(actor)
        {
            this.dieComponent = RequireComponent<DieComponent>();
            this.boundingRect = RequireComponent<BoundingRect>();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(this.boundingRect.Rect, Color.White);

            foreach (var position in this.dieComponent.PipPositions())
            {
                var radius = 32;
                var circle = new CircleF(position, radius);
                spriteBatch.DrawCircle(circle, 15, Color.Black, radius);
            }
        }
    }
}
