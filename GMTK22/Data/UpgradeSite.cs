using GMTK22.Components;

namespace GMTK22.Data
{
    public class UpgradeSite : UpgradeModule
    {
        public UpgradeSite(BuildingPosition position, BuildingMap map) : base(position, map, "Upgrade Site")
        {
            new BuildSiteRenderer(Actor);
            new UpgradeSiteRenderer(Actor, position, Map);
        }

        public override IBuildCommand[] Commands()
        {
            return new IBuildCommand[]
            {
                new BuildAutoRollerCommand(),
                new BuildSpeedUpgradeCommand(),
                new BuildWeightModuleCommand()
            };
        }
    }
}
