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
            private readonly ITwoColor twoColor;

            public BuildingBodyRenderer(Actor actor, ITwoColor twoColor) : base(actor)
            {
                this.twoColor = twoColor;
                this.boundingRect = RequireComponent<BoundingRect>();
            }

            public override void Draw(SpriteBatch spriteBatch)
            {
                var radius = this.boundingRect.Rect.Width / 2f;
                spriteBatch.DrawCircle(this.boundingRect.Rect.Center.ToVector2(), radius, 24, this.twoColor.PrimaryColor, radius, transform.Depth + 50);
                spriteBatch.DrawCircle(this.boundingRect.Rect.Center.ToVector2(), radius, 24, this.twoColor.SecondaryColor, 4f, transform.Depth + 40);
            }
        }
}
