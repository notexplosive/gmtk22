using GMTK22.Components;

namespace GMTK22.Data
{
    public abstract class UpgradeModule : SmallBuilding, IHasSpec
    {
        protected UpgradeModule(PositionAndMap positionAndMap, BuildingSpecification mySpec) : base(positionAndMap, mySpec.Name)
        {
            MySpec = mySpec;
        }

        public BuildingSpecification MySpec { get; }
    }
}
