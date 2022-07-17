using GMTK22.Components;

namespace GMTK22.Data
{
    public abstract class SmallBuilding : Building
    {
        protected SmallBuilding(PositionAndMap positionAndMap, string name) : base(positionAndMap, SmallBuilding.Size)
        {
        }
        
        public static readonly int Size = 32;
        
        public MainBuilding AttachedBuilding => Map.GetMainBuildingAt(Position.AsJustGridPosition());
        public float SpeedBoost { get; protected set; } = 0;
        public ProbableWeight ProbableWeight { get; protected set; } = new ProbableWeight();
        
        public SmallBuilding[] GetUpgrades()
        {
            return AttachedBuilding.GetSmallBuildings();
        }
    }
}
