using GMTK22.Data;

namespace GMTK22.Components
{
    public abstract class MainBuilding : Building
    {
        private readonly UpgradeModule[] cachedUpgrades;

        public MainBuilding(BuildingPosition position, string name, BuildingMap map) : base(position, name, map, 128)
        {
            this.cachedUpgrades = new UpgradeModule[8];
        }

        public abstract int CurrentFace { get; }

        public UpgradeModule[] Upgrades()
        {
            for (int i = 0; i < this.cachedUpgrades.Length; i++)
            {
                this.cachedUpgrades[i] = null;
            }

            var index = 0;
            foreach (var position in Position.AllSubgridPositions())
            {
                this.cachedUpgrades[index] = Map.GetUpgradeAt(position);
                index++;
            }

            return this.cachedUpgrades;
        }
        
        public abstract bool IsIdle();
        public abstract void Roll();
    }
}
