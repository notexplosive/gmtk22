using GMTK22.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GMTK22.Data
{
    public class UpgradeSite : UpgradeModule
    {
        public UpgradeSite(BuildingPosition position, BuildingMap map) : base(position, map, "Upgrade Site")
        {
            new BuildSiteRenderer(Actor);
            new UpgradeSiteRenderer(Actor, position, Map);
        }

        public override Command[] Commands()
        {
            return new Command[]
            {
                new ConstructBuildingCommand(Spec.AutoRoller),
                new ConstructBuildingCommand(Spec.SpeedModule),
                new ConstructBuildingCommand(Spec.WeightModule),
                new ConstructBuildingCommand(Spec.ReRollerModule)
            };
        }
    }

    public static class Spec
    {
        public static readonly BuildingSpecification AutoRoller =
            new BuildingSpecification("Build AutoRoller",
                info => new AutoRoller(info.Position, info.Map)
            );

        public static readonly BuildingSpecification SpeedModule =
            new BuildingSpecification("Build Speed Module",
                info => new SpeedUpgrade(info.Position, info.Map)
            );

        public static BuildingSpecification WeightModule =
            new BuildingSpecification("Build Weight Module",
                info => new WeightModule(info.Position, info.Map)
            );

        public static BuildingSpecification ReRollerModule =
            new BuildingSpecification("Build ReRoller",
                info => new ReRoller(info.Position, info.Map)
            );

        public static BuildingSpecification NormalDie =
            new BuildingSpecification("Build Die",
                info => new NormalDieBuilding(info.Position, info.Map),
                drawInfo =>
                {
                    drawInfo.spriteBatch.FillRectangle(drawInfo.rectangle, Color.White, drawInfo.depth);
                    var radius = drawInfo.rectangle.Width / 10;
                    drawInfo.spriteBatch.DrawCircle(new CircleF(drawInfo.rectangle.Center, radius), 8, Color.Black,
                        radius,
                        drawInfo.depth - 1);
                    var offsetSize = drawInfo.rectangle.Width / 5;
                    drawInfo.spriteBatch.DrawCircle(
                        new CircleF(drawInfo.rectangle.Center.ToVector2() + new Vector2(-offsetSize), radius), 8, Color.Black,
                        radius,
                        drawInfo.depth - 1);
                    drawInfo.spriteBatch.DrawCircle(
                        new CircleF(drawInfo.rectangle.Center.ToVector2() + new Vector2(offsetSize), radius),
                        8, Color.Black, radius, drawInfo.depth - 1);
                    drawInfo.spriteBatch.DrawCircle(
                        new CircleF(drawInfo.rectangle.Center.ToVector2() + new Vector2(-offsetSize, offsetSize), radius), 8,
                        Color.Black, radius, drawInfo.depth - 1);
                    drawInfo.spriteBatch.DrawCircle(
                        new CircleF(drawInfo.rectangle.Center.ToVector2() + new Vector2(offsetSize, -offsetSize), radius), 8,
                        Color.Black, radius, drawInfo.depth - 1);
                }
            );
    }
}
