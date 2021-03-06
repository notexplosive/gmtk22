using GMTK22.Components;
using GMTK22.Data.Buildings;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GMTK22.Data
{
    public class UpgradeSite : SmallBuilding
    {
        public override NameAndDescription NameAndDescription => new NameAndDescription("Upgrade Slot", "Can construct upgrades here");
        public UpgradeSite(PositionAndMap positionAndMap) : base(positionAndMap)
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
