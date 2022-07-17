using GMTK22.Components;

namespace GMTK22.Data
{
    public abstract class UpgradeModule : SmallBuilding, IHasSpec
    {
        protected UpgradeModule(PositionAndMap positionAndMap, BuildingSpecification mySpec) : base(positionAndMap)
        {
            MySpec = mySpec;
            
            new UpgradeModuleAttachmentRenderer(Actor, AttachedBuilding.Actor.transform.Position);
        }

        public override NameAndDescription NameAndDescription => new NameAndDescription(MySpec.Name, MySpec.Description);
        public BuildingSpecification MySpec { get; }
    }
}
