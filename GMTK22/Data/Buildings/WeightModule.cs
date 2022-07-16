using System;
using GMTK22.Components;

namespace GMTK22.Data.Buildings
{
    public class WeightModule : UpgradeModule
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification("Weight Module",
                info => new WeightModule(info),
                new Costs(200)
            );
        
        private readonly float percentageWeight;
        private readonly DieComponent dieComponent;

        public WeightModule(PositionAndMap positionAndMap) : base(positionAndMap, WeightModule.Spec)
        {
            this.percentageWeight = 0.1f;
            this.dieComponent = new DieComponent(Actor, DieCartridge.GameCore.CleanRandom, AttachedBuilding.Faces, GetUpgrades);
            new DieRenderer(Actor, Palette.WeightBody, Palette.WeightPips);

            dieComponent.ForceRoll();
            dieComponent.RollFinished += AssignWeight;
        }

        private void AssignWeight(Roll roll)
        {
            ProbableWeight = new ProbableWeight(roll.FaceValue, this.percentageWeight);
        }

        public override Command[] Commands()
        {
            return new Command[]
            {
                new CallbackCommand("ReRoll", ReRollerModule.Spec.Costs.ConstructCost / 2,
                    () => { this.dieComponent.ForceRoll(); })
            };
        }
    }
}
