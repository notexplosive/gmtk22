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
            spriteBatch.FillRectangle(this.boundingRect.Rect, Color.Black.WithMultipliedOpacity(0.25f), transform.Depth);
        }
    }
}
