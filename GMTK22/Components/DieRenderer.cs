using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GMTK22.Components
{
    public class DieRenderer : BaseComponent
    {
        private readonly Color bodyColor;
        private readonly Color pipColor;
        private readonly DieComponent die;
        private readonly BoundingRect boundingRect;

        public DieRenderer(Actor actor, Color bodyColor, Color pipColor) : base(actor)
        {
            this.bodyColor = bodyColor;
            this.pipColor = pipColor;
            this.boundingRect = RequireComponent<BoundingRect>();
            this.die = RequireComponent<DieComponent>();
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawDie(spriteBatch, this.boundingRect.Rect, this.die.Pips, this.bodyColor, this.pipColor, transform.Depth);
        }

        public static void DrawDie(SpriteBatch spriteBatch, Rectangle rect, Pip[] pips, Color bodyColor, Color pipColor, Depth depth)
        {
            spriteBatch.FillRectangle(rect, bodyColor, depth);

            foreach (var pip in pips)
            {
                var radius = rect.Width / 10;
                var circle = new CircleF(rect.Center.ToVector2() + pip.LocalPosition.Value * rect.Width / 4f, radius);
                spriteBatch.DrawCircle(circle, 10, pipColor, radius, depth - 10);
            }

            spriteBatch.DrawRectangle(rect, pipColor, 3f, depth - 1);
        }
    }
}
