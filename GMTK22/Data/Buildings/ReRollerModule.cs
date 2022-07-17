using GMTK22.Components;

namespace GMTK22.Data.Buildings
{
    public class ReRollerModule : UpgradeModule
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification(new NameAndDescription("Re-roller", "Rolls a die on construction, if the attached die matches that number, immediately re-roll attached die."),
                info => new ReRollerModule(info),
                new Costs(100)
            );
        
        private readonly DieComponent dieComponent;

        public ReRollerModule(PositionAndMap positionAndMap) : base(positionAndMap, ReRollerModule.Spec)
        {
            this.dieComponent = new DieComponent(Actor, DieCartridge.GameCore.CleanRandom, AttachedBuilding.Faces, GetUpgrades);
            new DieRenderer(Actor, Palette.ReRollerBody, Palette.ReRollerPips);
            new ReRollerComponent(Actor, Position, Map);
            
            this.dieComponent.ForceRoll();
            this.dieComponent.RollFinished += AssignRollingCriteria;
        }

        private void AssignRollingCriteria(Roll roll)
        {
            FaceToReRollOn = roll.FaceValue;
        }

        public int FaceToReRollOn { get; set; } = 0;

        public override Command[] Commands()
        {
            return new Command[]
            {
                new CallbackCommand(new NameAndDescription("Re-roll", "Re-roll the number on this die"), ReRollerModule.Spec.Costs.ConstructCost / 2,() => { this.dieComponent.ForceRoll(); })
            };
        }
    }
}
