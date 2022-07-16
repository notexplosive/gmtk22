using GMTK22.Components;

namespace GMTK22.Data
{
    public abstract class UpgradeModule : Building
    {
        protected UpgradeModule(BuildingPosition position, BuildingMap map, string name) : base(position, name, map, UpgradeModule.Size)
        {
        }

        public MainBuilding AttachedBuilding => Map.GetMainBuildingAt(Position.AsJustGridPosition());
        public static int Size { get; } = 32;

        public UpgradeModule[] GetUpgrades()
        {
            return AttachedBuilding.Upgrades();
        }
        public float SpeedBoost { get; protected set; } = 0;
        public ProbableWeight ProbableWeight { get; protected set; } = new ProbableWeight();
    }
}
