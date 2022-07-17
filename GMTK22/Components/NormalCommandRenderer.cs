using GMTK22.Data;
using Machina.Data;
using Machina.Data.TextRendering;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTK22.Components
{
    public class NormalCommandRenderer : CommandRenderer
    {
        private readonly Command command;

        public NormalCommandRenderer(Actor actor, Command command, char hotkey) : base(actor, hotkey)
        {
            this.command = command;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var cost = this.command.Cost;
            var rect = RenderCostAndBackgroundRect(spriteBatch, cost);

            var font = new SpriteFontMetrics(MachinaClient.Assets.GetSpriteFont("UIFontSmall"));
            var text = new BoundedText(rect.Size, Alignment.Center, Overflow.Ignore,
                new FormattedText(
                    new FormattedTextFragment(this.command.NameAndDescription.Name, font, Color.Black)
                )
            );

            text.Draw(spriteBatch, rect.Location);
        }
    }
}
