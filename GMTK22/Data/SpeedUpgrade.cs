using System;
using GMTK22.Components;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GMTK22.Data
{
    public class SpeedUpgrade : UpgradeModule
    {
        public SpeedUpgrade(BuildingPosition position, BuildingMap map) : base(position, map, "Auto Roller")
        {
            SpeedBoost = 1;
            new BuildingBodyRenderer(Actor);
            new SpeedUpgradeRenderer(Actor, map.GetMainBuildingAt(new BuildingPosition(position.GridPosition)));
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
