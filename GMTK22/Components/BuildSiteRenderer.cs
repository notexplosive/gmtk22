using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GMTK22.Components
{
    public class BuildSiteRenderer : BaseComponent
    {
        private readonly BoundingRect boundingRect;

        public BuildSiteRenderer(Actor actor) : base(actor)
        {
            this.boundingRect = RequireComponent<BoundingRect>();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(this.boundingRect.Rect, Color.Blue.WithMultipliedOpacity(0.25f), 4f, transform.Depth);
        }
    }
}
