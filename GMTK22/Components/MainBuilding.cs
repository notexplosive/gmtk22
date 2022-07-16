using System;
using GMTK22.Data;

namespace GMTK22.Components
{
    public abstract class MainBuilding : Building, IHasSpec
    {
        private readonly SmallBuilding[] cachedUpgrades;
        protected readonly DieComponent dieComponent;

        public MainBuilding(BuildingPosition position, BuildingMap map, string name, int[] faces,
            BuildingSpecification spec) : base(position, name, map)
        {
            Faces = faces;
            this.cachedUpgrades = new SmallBuilding[8];
            MySpec = spec;

            this.dieComponent = new DieComponent(Actor, DieCartridge.GameCore.CleanRandom, Faces, GetSmallBuildings);
            new RollOnHover(Actor);
            new DieRenderer(Actor, Palette.NormalDieBody, Palette.NormalDiePips);
        }

        public int CurrentFace => this.dieComponent.CurrentFace;
        public int[] Faces { get; }

        public BuildingSpecification MySpec { get; }

        public override Command[] Commands()
        {
            return Array.Empty<Command>();
        }

        public bool IsIdle()
        {
            return this.dieComponent.IsTweenDone();
        }

        public void Roll()
        {
            this.dieComponent.AttemptToRoll();
        }

        public SmallBuilding[] GetSmallBuildings()
        {
            for (var i = 0; i < this.cachedUpgrades.Length; i++)
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
    }
}
