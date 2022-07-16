using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GMTK22.Components
{
        public class BuildingBodyRenderer : BaseComponent
        {
            private readonly BoundingRect boundingRect;

            public BuildingBodyRenderer(Actor actor) : base(actor)
            {
                this.boundingRect = RequireComponent<BoundingRect>();
            }

            public override void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.FillRectangle(this.boundingRect.Rect, Color.White, transform.Depth + 50);
            }
        }
}
