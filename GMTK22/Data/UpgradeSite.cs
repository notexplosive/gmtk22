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
                new ConstructBuildingCommand(AutoRoller.Spec),
                new ConstructBuildingCommand(SpeedUpgrade.Spec),
                new ConstructBuildingCommand(WeightModule.Spec),
                new ConstructBuildingCommand(ReRollerModule.Spec)
            };
        }
    }
}
