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

        public static readonly BuildingSpecification WeightModule =
            new BuildingSpecification("Build Weight Module",
                info => new WeightModule(info.Position, info.Map)
            );

        public static readonly BuildingSpecification ReRollerModule =
            new BuildingSpecification("Build ReRoller",
                info => new ReRoller(info.Position, info.Map)
            );

        public static readonly BuildingSpecification NormalDie =
            new BuildingSpecification("Build Die",
                info => new NormalDieBuilding(info.Position, info.Map),
                drawInfo =>
                {
                    
                }
            );
    }
}
