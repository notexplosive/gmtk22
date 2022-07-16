using GMTK22.Data;

namespace GMTK22.Components
{
    public abstract class MainBuilding : Building, IHasSpec
    {
        private readonly SmallBuilding[] cachedUpgrades;

        public MainBuilding(BuildingPosition position, string name, BuildingMap map, int[] faces, BuildingSpecification spec) : base(position, name, map, 128)
        {
            Faces = faces;
            this.cachedUpgrades = new SmallBuilding[8];
            MySpec = spec;
        }

        public BuildingSpecification MySpec { get; }

        public abstract int CurrentFace { get; }
        public int[] Faces { get; }

        public SmallBuilding[] GetSmallBuildings()
        {
            for (int i = 0; i < this.cachedUpgrades.Length; i++)
            {
                this.cachedUpgrades[i] = null;
            }

            var index = 0;
            foreach (var position in Position.AllSubgridPositions())
            {
                this.cachedUpgrades[index] = Map.GetSmallBuildingAt(position);
                index++;
            }

            return this.cachedUpgrades;
        }
        
        public abstract bool IsIdle();
        public abstract void Roll();
    }
}
