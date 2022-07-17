using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GMTK22.Components
{
    public class UpgradeModuleAttachmentRenderer : BaseComponent
    {
        private readonly Vector2 targetPosition;

        public UpgradeModuleAttachmentRenderer(Actor actor, Vector2 targetPosition) : base(actor)
        {
            this.targetPosition = targetPosition;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var sourcePos = this.actor.transform.Position;
            var displacement = this.targetPosition - sourcePos;

            var radius = 25f;
            spriteBatch.DrawCircle(new CircleF(sourcePos, radius), 15, Palette.CableColor, 3f, transform.Depth + 100);
            spriteBatch.DrawLine(sourcePos + displacement.NormalizedCopy() * radius, this.targetPosition, Palette.CableColor, 3f, transform.Depth + 100);
        }
    }
}
