using System;
using GMTK22.Data;
using Microsoft.Xna.Framework;

namespace GMTK22.Components
{
    public interface ITwoColor
    {
        public Color PrimaryColor { get; }
        public Color SecondaryColor { get; }
    }

    public class ColorPair : ITwoColor
    {
        public ColorPair(Color primaryColor, Color secondaryColor)
        {
            PrimaryColor = primaryColor;
            SecondaryColor = secondaryColor;
        }

        public Color PrimaryColor { get; }
        public Color SecondaryColor { get; }
    }
    
    public readonly struct DieData : ITwoColor
    {
        public int[] Faces { get; }
        public Color BodyColor { get; }
        public Color PipColor { get; }
        public Color PrimaryColor => BodyColor;
        public Color SecondaryColor => PipColor;
        public float Duration { get; }
        public PlayableSoundEffect Sound { get; }

        public DieData(int[] faces, Color bodyColor, Color pipColor, PlayableSoundEffect sound, float duration = 1.5f)
        {
            Faces = faces;
            BodyColor = bodyColor;
            PipColor = pipColor;
            Duration = duration;
            Sound = sound;
        }
    }
    
    public abstract class MainBuilding : Building, IHasSpec
    {
        private readonly SmallBuilding[] cachedUpgrades;
        protected readonly DieComponent dieComponent;

        public MainBuilding(PositionAndMap positionAndMap, DieData dieData) : base(positionAndMap)
        {
            DieData = dieData;
            this.cachedUpgrades = new SmallBuilding[8];

            this.dieComponent = new DieComponent(Actor, DieCartridge.GameCore.CleanRandom,  Faces, dieData.Duration, dieData.Sound, GetSmallBuildings);
            new RollOnHover(Actor);
            new DieRenderer(Actor, dieData.BodyColor, dieData.PipColor);
        }

        public DieData DieData { get; }

        public int CurrentFace => this.dieComponent.CurrentFace;
        public int[] Faces => DieData.Faces;

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
