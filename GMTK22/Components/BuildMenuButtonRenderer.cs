using GMTK22.Data;
using Machina.Components;
using Machina.Data;
using Machina.Data.TextRendering;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GMTK22.Components
{
    public class BuildMenuButtonRenderer : BaseComponent
    {
        private readonly BoundingRect boundingRect;
        private readonly IBuildCommand command;
        private readonly Hoverable hoverable;

        public BuildMenuButtonRenderer(Actor actor, IBuildCommand command) : base(actor)
        {
            this.boundingRect = RequireComponent<BoundingRect>();
            this.hoverable = RequireComponent<Hoverable>();
            this.command = command;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var font = new SpriteFontMetrics(MachinaClient.Assets.GetSpriteFont("UIFontSmall"));
            var rect = this.boundingRect.Rect;
            
            spriteBatch.FillRectangle(rect, this.hoverable.IsHovered ? Palette.BuildButtonHoveredColor : Palette.BuildButtonColor , transform.Depth);
            
            rect.Inflate(-5, -5);

            var text = new BoundedText(rect.Size, Alignment.TopRight, Overflow.Ignore,
                new FormattedText(
                    new FormattedTextFragment("Cost: ", font, Color.Black),
                    new FormattedTextFragment("5", font, Color.Goldenrod)
                )
            );

            text.Draw(spriteBatch, rect.Location);
            
            if (!this.hoverable.IsHovered)
            {
                rect.Inflate(-10, -10);
            }
            else
            {
                rect.Inflate(-5, -5);
            }
            

            this.command.DrawButtonGraphic(spriteBatch, rect, transform.Depth - 10);
        }
    }
}
