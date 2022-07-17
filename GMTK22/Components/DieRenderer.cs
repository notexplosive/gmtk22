using ExTween;
using GMTK22.Data;
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
        private readonly BoundingRect boundingRect;
        private readonly DieComponent die;
        private readonly Color pipColor;

        public DieRenderer(Actor actor, Color bodyColor, Color pipColor) : base(actor)
        {
            this.bodyColor = bodyColor;
            this.pipColor = pipColor;
            this.boundingRect = RequireComponent<BoundingRect>();
            this.die = RequireComponent<DieComponent>();
        }

        public static void GenericDrawDie1Pip(DrawInfo drawInfo)
        {
            var pips = new[] {new Pip()};
            
            DieRenderer.DrawDie(drawInfo.SpriteBatch, drawInfo.Rectangle, pips,
                drawInfo.PrimaryColor, drawInfo.SecondaryColor, drawInfo.Depth);
        }

        public static void GenericDrawDie3Pips(DrawInfo drawInfo)
        {
            var pips = new[] {new Pip(), new Pip(), new Pip()};
            pips[0].LocalPosition.ForceSetValue(Pip.TopRight);
            pips[1].LocalPosition.ForceSetValue(Pip.BottomLeft);
            
            DieRenderer.DrawDie(drawInfo.SpriteBatch, drawInfo.Rectangle, pips,
                drawInfo.PrimaryColor, drawInfo.SecondaryColor, drawInfo.Depth);
        }

        public static void GenericDrawDie5Pips(DrawInfo drawInfo)
        {
            var pips = new[] {new Pip(), new Pip(), new Pip(), new Pip(), new Pip()};
            pips[0].LocalPosition.ForceSetValue(Pip.TopRight);
            pips[1].LocalPosition.ForceSetValue(Pip.BottomLeft);
            pips[2].LocalPosition.ForceSetValue(Pip.TopLeft);
            pips[3].LocalPosition.ForceSetValue(Pip.BottomRight);
            
            DieRenderer.DrawDie(drawInfo.SpriteBatch, drawInfo.Rectangle,
                pips,
                drawInfo.PrimaryColor, drawInfo.SecondaryColor, drawInfo.Depth);
        }

        public static void GenericDrawDie7Pips(DrawInfo drawInfo)
        {
            var pips = new[] {new Pip(), new Pip(), new Pip(), new Pip(), new Pip(), new Pip(), new Pip()};
            pips[0].LocalPosition.ForceSetValue(Pip.TopRight);
            pips[1].LocalPosition.ForceSetValue(Pip.BottomLeft);
            pips[2].LocalPosition.ForceSetValue(Pip.TopLeft);
            pips[3].LocalPosition.ForceSetValue(Pip.BottomRight);
            pips[4].LocalPosition.ForceSetValue(Pip.Left);
            pips[5].LocalPosition.ForceSetValue(Pip.Right);
            
            DieRenderer.DrawDie(drawInfo.SpriteBatch, drawInfo.Rectangle,
                pips,
                drawInfo.PrimaryColor, drawInfo.SecondaryColor, drawInfo.Depth);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DieRenderer.DrawDie(spriteBatch, this.boundingRect.Rect, this.die.Pips, this.bodyColor, this.pipColor,
                transform.Depth);
        }

        public static void DrawDie(SpriteBatch spriteBatch, Rectangle rect, Pip[] pips, Color bodyColor, Color pipColor,
            Depth depth)
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
