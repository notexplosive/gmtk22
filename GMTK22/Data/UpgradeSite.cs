using GMTK22.Components;

namespace GMTK22.Data
{
    public class UpgradeSite : Building
    {
        public UpgradeSite(BuildingPosition position, BuildingMap map) : base(position, "Upgrade Site", map, buildingSize: UpgradeSite.Size)
        {
            new BuildSiteRenderer(Actor);
        }

        public static int Size { get; } = 32;

        public override IBuildCommand[] Commands => new IBuildCommand[]
        {
            new BuildAutoRollerCommand()
        };
    }
}
