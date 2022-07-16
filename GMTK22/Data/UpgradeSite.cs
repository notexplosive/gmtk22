using GMTK22.Components;
using GMTK22.Data.Buildings;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GMTK22.Data
{
    public class UpgradeSite : SmallBuilding
    {
        public UpgradeSite(PositionAndMap positionAndMap) : base(positionAndMap, "Upgrade Site")
        {
            new BuildSiteRenderer(Actor);
            new UpgradeSiteRenderer(Actor, Position, Map);
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
