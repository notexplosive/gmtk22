using System;
using GMTK22.Components;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GMTK22.Data.Buildings
{
    public class SpeedUpgrade : UpgradeModule
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification(
                new NameAndDescription("Speed Module", "Decrease roll duration of attached die by 0.1 seconds"),
                new Costs(100), new ColorPair(Palette.SpeedUpgradeBody, Palette.SpeedUpgradeIcon),
                info => new SpeedUpgrade(info),
                SpeedUpgrade.DrawIcon);

        public SpeedUpgrade(PositionAndMap positionAndMap) : base(positionAndMap, SpeedUpgrade.Spec)
        {
            SpeedBoost = 1;
            new BuildingBodyRenderer(Actor, MySpec.Colors);
            new SpeedUpgradeRenderer(Actor, Map.GetMainBuildingAt(new BuildingPosition(Position.GridPosition)));
        }

        private static void DrawIcon(DrawInfo drawInfo)
        {
            var smallRadius = drawInfo.Rectangle.Width / 3;
            var bigRadius = drawInfo.Rectangle.Width / 2;

            drawInfo.SpriteBatch.DrawCircle(new CircleF(drawInfo.Rectangle.Center.ToVector2(), bigRadius), 20,
                drawInfo.PrimaryColor, bigRadius, drawInfo.Depth);

            drawInfo.SpriteBatch.DrawLine(
                new Vector2(0,smallRadius) + drawInfo.Rectangle.Center.ToVector2(),
                new Vector2(0,-smallRadius) + drawInfo.Rectangle.Center.ToVector2(), 
                drawInfo.SecondaryColor, 8f, drawInfo.Depth - 10);
            
            drawInfo.SpriteBatch.DrawLine(
                new Vector2(smallRadius,0) + drawInfo.Rectangle.Center.ToVector2(),
                new Vector2(-smallRadius,0) + drawInfo.Rectangle.Center.ToVector2(), 
                drawInfo.SecondaryColor, 8f, drawInfo.Depth - 10);
        }

        public override Command[] Commands()
        {
            return Array.Empty<Command>();
        }
    }

    public class SpeedUpgradeRenderer : BaseComponent
    {
        private readonly BoundingRect boundingRect;

        public SpeedUpgradeRenderer(Actor actor, MainBuilding mainBuilding) : base(actor)
        {
            this.boundingRect = RequireComponent<BoundingRect>();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var rect = this.boundingRect.Rect;
            spriteBatch.DrawLine(
                rect.Center.ToVector2() + new Vector2(0, rect.Height / 3f),
                rect.Center.ToVector2() - new Vector2(0, rect.Height / 3f),
                Color.Orange, 4, transform.Depth - 5);

            spriteBatch.DrawLine(
                rect.Center.ToVector2() + new Vector2(rect.Width / 3f, 0),
                rect.Center.ToVector2() - new Vector2(rect.Width / 3f, 0),
                Color.Orange, 4, transform.Depth - 5);
        }
    }
}
