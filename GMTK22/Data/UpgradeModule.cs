using GMTK22.Components;

namespace GMTK22.Data
{
    public abstract class UpgradeModule : SmallBuilding, IHasSpec
    {
        protected UpgradeModule(BuildingPosition position, BuildingMap map, string name, BuildingSpecification mySpec) : base(position, map, name)
        {
            MySpec = mySpec;
        }

        public BuildingSpecification MySpec { get; }
    }
}
