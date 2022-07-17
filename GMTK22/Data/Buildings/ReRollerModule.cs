using GMTK22.Components;

namespace GMTK22.Data.Buildings
{
    public class ReRollerModule : UpgradeModule
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification(
                new NameAndDescription("Re-roller",
                    "Rolls a die on construction, if the attached die matches that number, immediately re-roll attached die."),
                new Costs(40), new ColorPair(Palette.ReRollerBody, Palette.ReRollerPips),
                info => new ReRollerModule(info), DieRenderer.GenericDrawDie1Pip);

        private readonly DieComponent dieComponent;

        public ReRollerModule(PositionAndMap positionAndMap) : base(positionAndMap, ReRollerModule.Spec)
        {
            this.dieComponent = new DieComponent(Actor, DieCartridge.GameCore.CleanRandom, AttachedBuilding.Faces, 0.5f, DieCartridge.loBlockSounds.GetNext(),
                GetUpgrades);
            new DieRenderer(Actor, Palette.ReRollerBody, Palette.ReRollerPips);
            new ReRollerComponent(Actor, Position, Map);

            this.dieComponent.ForceRoll();
            this.dieComponent.RollFinished += AssignRollingCriteria;
        }

        public int FaceToReRollOn { get; set; }

        private void AssignRollingCriteria(Roll roll)
        {
            FaceToReRollOn = roll.FaceValue;
        }

        public override Command[] Commands()
        {
            return new Command[]
            {
                new CallbackCommand(new NameAndDescription("Re-roll", "Re-roll the number on this die"),
                    ReRollerModule.Spec.Costs.ConstructCost / 2, () => { this.dieComponent.ForceRoll(); })
            };
        }
    }
}
