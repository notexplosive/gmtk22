using GMTK22.Components;

namespace GMTK22.Data
{
    public class UpgradeSite : UpgradeModule
    {
        public UpgradeSite(BuildingPosition position, BuildingMap map) : base(position, "Upgrade Site", map)
        {
            new BuildSiteRenderer(Actor);
        }
        
        public override IBuildCommand[] Commands => new IBuildCommand[]
        {
            new BuildAutoRollerCommand(),
            new BuildSpeedUpgradeCommand()
        };
    }
}
