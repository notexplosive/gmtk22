using System;
using GMTK22.Data;
using Microsoft.Xna.Framework;

namespace GMTK22.Components
{
    public readonly struct DieData
    {
        public int[] Faces { get; }
        public Color BodyColor { get; }
        public Color PipColor { get; }

        public DieData(int[] faces, Color bodyColor, Color pipColor)
        {
            Faces = faces;
            BodyColor = bodyColor;
            PipColor = pipColor;
        }
    }
    
    public abstract class MainBuilding : Building, IHasSpec
    {
        private readonly SmallBuilding[] cachedUpgrades;
        protected readonly DieComponent dieComponent;

        public MainBuilding(PositionAndMap positionAndMap, DieData dieData) : base(positionAndMap)
        {
            Faces = dieData.Faces;
            this.cachedUpgrades = new SmallBuilding[8];

            this.dieComponent = new DieComponent(Actor, DieCartridge.GameCore.CleanRandom, Faces, GetSmallBuildings);
            new RollOnHover(Actor);
            new DieRenderer(Actor, dieData.BodyColor, dieData.PipColor);
        }

        public int CurrentFace => this.dieComponent.CurrentFace;
        public int[] Faces { get; }

        public override NameAndDescription NameAndDescription => new NameAndDescription(MySpec.Name, MySpec.Description);

        public abstract BuildingSpecification MySpec { get; }

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
