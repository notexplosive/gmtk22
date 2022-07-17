using System;
using ExTween;
using GMTK22.Components;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GMTK22.Data.Buildings
{
    public class AutoRoller : UpgradeModule
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification(new NameAndDescription("Build AutoRoller", "Automatically rolls attached die every 5 seconds"),
                new Costs(20), new ColorPair(Palette.AutorollerBody, Palette.AutorollerProgress), 
                info => new AutoRoller(info),
                AutoRoller.DrawIcon
                );

        private static void DrawIcon(DrawInfo drawInfo)
        {
            var smallRadius = drawInfo.Rectangle.Width / 3;
            var bigRadius = drawInfo.Rectangle.Width / 2;
            
            drawInfo.SpriteBatch.DrawCircle(new CircleF(drawInfo.Rectangle.Center.ToVector2(), bigRadius), 10, drawInfo.PrimaryColor, bigRadius, drawInfo.Depth);
            drawInfo.SpriteBatch.DrawCircle(new CircleF(drawInfo.Rectangle.Center.ToVector2(), smallRadius), 10, drawInfo.SecondaryColor, 3f, drawInfo.Depth - 10);
        }

        public AutoRoller(PositionAndMap positionAndMap) : base(positionAndMap, AutoRoller.Spec)
        {
            new UpgradeBuildingBodyRenderer(Actor, MySpec.Colors);
            new AutoRollerComponent(Actor, Map.GetMainBuildingAt(new BuildingPosition(Position.GridPosition)), MySpec.Colors);
        }

        public override Command[] Commands()
        {
            return Array.Empty<Command>();
        }
    }
    
    public class AutoRollerComponent : BaseComponent
    {
        private readonly MainBuilding targetBuilding;
        private readonly float maxCooldown;
        private float cooldown;
        private readonly BoundingRect boundingRect;
        private readonly ITwoColor colors;
        private readonly UpgradeBuildingBodyRenderer body;

        public AutoRollerComponent(Actor actor, MainBuilding targetBuilding, ITwoColor colors) : base(actor)
        {
            this.boundingRect = RequireComponent<BoundingRect>();
            this.targetBuilding = targetBuilding;
            this.maxCooldown = 5f;
            this.cooldown = this.maxCooldown;
            this.colors = colors;
            this.body = RequireComponent<UpgradeBuildingBodyRenderer>();
        }

        public override void Update(float dt)
        {
            if (this.cooldown > 0)
            {
                this.cooldown -= dt;
            }

            if (this.cooldown <= 0)
            {
                if (this.targetBuilding.IsIdle())
                {
                    this.body.TriggerPulse();
                    this.targetBuilding.Roll();
                    this.cooldown = this.maxCooldown;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var rect = this.boundingRect.Rect;
            var percent = 1f - (this.cooldown / this.maxCooldown);
            var radius = rect.Width * 0.25f;
            radius *= percent;

            spriteBatch.DrawCircle(new CircleF(rect.Center.ToVector2(),radius), 16, this.colors.SecondaryColor, radius, transform.Depth - 10);
        }
    }
}
