using GMTK22.Components;

namespace GMTK22.Data.Buildings
{
    public class WeightModule : UpgradeModule
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification(
                new NameAndDescription("Weight Module",
                    "Rolls a die, ALL attached dice (including other weights) are 10% more likely to roll that number."),
                new Costs(30), new ColorPair(Palette.WeightBody, Palette.WeightPips), info => new WeightModule(info),
                DieRenderer.GenericDrawDie3Pips);

        private readonly DieComponent dieComponent;

        private readonly float percentageWeight;

        public WeightModule(PositionAndMap positionAndMap) : base(positionAndMap, WeightModule.Spec)
        {
            this.percentageWeight = 0.1f;
            this.dieComponent = new DieComponent(Actor, DieCartridge.GameCore.CleanRandom, AttachedBuilding.Faces, 0.5f, GetUpgrades);
            new DieRenderer(Actor, Palette.WeightBody, Palette.WeightPips);

            this.dieComponent.ForceRoll();
            this.dieComponent.RollFinished += AssignWeight;
        }

        private void AssignWeight(Roll roll)
        {
            ProbableWeight = new ProbableWeight(roll.FaceValue, this.percentageWeight);
        }

        public override Command[] Commands()
        {
            return new Command[]
            {
                new CallbackCommand(new NameAndDescription("Re-roll", "Re-roll the face value of this die"),
                    ReRollerModule.Spec.Costs.ConstructCost / 2,
                    () =>
                    {
                        ProbableWeight = new ProbableWeight(); // empty
                        this.dieComponent.ForceRoll();
                    })
            };
        }
    }
}
