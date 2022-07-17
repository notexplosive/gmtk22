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
            new BuildingSpecification(new NameAndDescription("Speed Module","Decrease roll duration of attached die by 0.1 seconds"),
                new Costs(100), info => new SpeedUpgrade(info));
        
        public SpeedUpgrade(PositionAndMap positionAndMap) : base(positionAndMap, SpeedUpgrade.Spec)
        {
            SpeedBoost = 1;
            new BuildingBodyRenderer(Actor);
            new SpeedUpgradeRenderer(Actor, Map.GetMainBuildingAt(new BuildingPosition(Position.GridPosition)));
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
                rect.Center.ToVector2() + new Vector2(0,rect.Height / 3f), 
                rect.Center.ToVector2() - new Vector2(0, rect.Height / 3f), 
                Color.Orange, 4, transform.Depth - 5);
            
            spriteBatch.DrawLine(
                rect.Center.ToVector2() + new Vector2(rect.Width / 3f, 0), 
                rect.Center.ToVector2() - new Vector2(rect.Width / 3f, 0), 
                Color.Orange, 4, transform.Depth - 5);
        }
    }
}
