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
    public class BuildMainBuildingButtonRenderer : BaseComponent
    {
        private readonly BoundingRect boundingRect;
        private readonly Hoverable hoverable;
        private readonly BuildingSpecification spec;

        public BuildMainBuildingButtonRenderer(Actor actor, BuildingSpecification spec) : base(actor)
        {
            this.boundingRect = RequireComponent<BoundingRect>();
            this.hoverable = RequireComponent<Hoverable>();
            this.spec = spec;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var font = new SpriteFontMetrics(MachinaClient.Assets.GetSpriteFont("UIFontSmall"));
            var rect = this.boundingRect.Rect;
            
            spriteBatch.FillRectangle(rect, this.hoverable.IsHovered ? Palette.BuildButtonHoveredColor : Palette.BuildButtonColor , transform.Depth);
            
            rect.Inflate(-5, -5);

            var cost = this.spec.Costs.ConstructCost;
            var text = new BoundedText(rect.Size, Alignment.TopRight, Overflow.Ignore,
                new FormattedText(
                    new FormattedTextFragment(cost.ToString(), font, DieCartridge.GameCore.Player.CanAfford(cost) ? Palette.MoneyColor : Color.DarkRed)
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

            this.spec.Draw(spriteBatch, rect, transform.Depth - 50);
        }
    }
}
