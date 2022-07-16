using System;
using GMTK22.Components;

namespace GMTK22.Data
{
    public class WeightModule : UpgradeModule
    {
        private readonly float percentageWeight;

        public WeightModule(BuildingPosition position, BuildingMap map) : base(position, map, "Weight Upgrade")
        {
            this.percentageWeight = 0.1f;
            var dieComponent = new DieComponent(Actor, DieCartridge.GameCore.CleanRandom, GetUpgrades);
            new DieRenderer(Actor);

            dieComponent.ForceRoll();
            dieComponent.RollFinished += AssignWeight;
        }

        private void AssignWeight(Roll roll)
        {
            Weight = new Weight(roll.FaceValue, this.percentageWeight);
        }

        public override IBuildCommand[] Commands()
        {
            return Array.Empty<IBuildCommand>();
        }
    }
}
