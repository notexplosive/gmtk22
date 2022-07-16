namespace GMTK22.Data
{
    public abstract class UpgradeModule : Building
    {
        protected UpgradeModule(BuildingPosition position, string name, BuildingMap map) : base(position, name, map, UpgradeSite.Size)
        {
        }
    }
}
