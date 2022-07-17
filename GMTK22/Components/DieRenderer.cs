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
        private readonly bool firstDie;

        public DieRenderer(Actor actor, Color bodyColor, Color pipColor) : base(actor)
        {
            this.bodyColor = bodyColor;
            this.pipColor = pipColor;
            this.boundingRect = RequireComponent<BoundingRect>();
            this.die = RequireComponent<DieComponent>();

            this.firstDie = Progression.IsFirstDie();
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
            if (Progression.NumberOfTotalDieRolls <= 3 && this.firstDie)
            {
                var font = MachinaClient.Assets.GetSpriteFont("UIFontSmall");
                var text = "Hover over dice to roll them!";
                var textSize = font.MeasureString("Hover over dice to roll them!");
                spriteBatch.DrawString(font, text, transform.Position + new Vector2(0, -100), Color.White, 0f,
                    textSize / 2, Vector2.One, SpriteEffects.None, transform.Depth - 100);
            }

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
