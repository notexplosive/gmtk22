using GMTK22.Data;
using Machina.Components;
using Machina.Data;
using Machina.Data.TextRendering;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTK22.Components
{
    public class Tooltip : BaseComponent
    {
        private readonly BoundingRect boundingRect;
        private readonly BuildMenu buildMenu;
        private BoundedText boundedText;
        private bool textIsSet = false;

        public Tooltip(Actor actor, BuildMenu buildMenu) : base(actor)
        {
            this.boundingRect = RequireComponent<BoundingRect>();
            this.buildMenu = buildMenu;
        }

        public override void Update(float dt)
        {
            var hoveredCommand = this.buildMenu.GetHoveredCommand();
            if (hoveredCommand != null)
            {
                SetText(hoveredCommand.NameAndDescription, hoveredCommand.Cost);
            }
            else
            {
                ClearText();
            }
        }

        private void ClearText()
        {
            this.textIsSet = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.textIsSet)
            {
                this.boundedText.Draw(spriteBatch, this.boundingRect.TopLeft.ToPoint());
            }
        }

        private void SetText(NameAndDescription nameAndDescription, int cost)
        {
            var titleFont = new SpriteFontMetrics(MachinaClient.Assets.GetSpriteFont("UIFont"));
            var textFont = new SpriteFontMetrics(MachinaClient.Assets.GetSpriteFont("UIFontSmall"));
            this.textIsSet = true;
            this.boundedText = new BoundedText(this.boundingRect.Size, Alignment.TopLeft, Overflow.Ignore,
                new FormattedText(
                    new FormattedTextFragment(nameAndDescription.Name, titleFont, Color.White),
                    new FormattedTextFragment("\nCost: ", textFont, Color.White),
                    new FormattedTextFragment(cost + " pips", textFont, DieCartridge.GameCore.Player.CanAfford(cost) ? Palette.MoneyColor : Color.DarkRed),
                    new FormattedTextFragment("\n" + nameAndDescription.Description, textFont, Color.White)
                )
                );
        }
    }
}
